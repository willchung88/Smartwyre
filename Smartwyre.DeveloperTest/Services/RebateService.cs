using Smartwyre.DeveloperTest.Calculators;
using System.Collections.Generic;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly IRebateDataStore rebateDataStore;
    private readonly IProductDataStore productDataStore;
    private readonly List<IRebateCalculator> rebateCalculators;


    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore)
    {
        this.rebateDataStore = rebateDataStore;
        this.productDataStore = productDataStore;
        rebateCalculators = new List<IRebateCalculator>
        {
            new FixedCashAmountRebateCalculator(),
            new FixedRateRebateCalculator(),
            new AmountPerUomRebateCalculator()
        };
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();

        var rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = productDataStore.GetProduct(request.ProductIdentifier);

        if (rebate == null || product == null)
        {
            result.Success = false;
            return result;
        }

        decimal rebateAmount = 0m;

        foreach (var calculator in rebateCalculators)
        {
            if (!result.Success && calculator.CanCalculate(rebate, product, request))
            {
                result.Success = true;
                rebateAmount = calculator.CalculateRebateAmount(rebate, product, request);
            }
        }

        if (result.Success)
            rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

        return result;
    }

}
