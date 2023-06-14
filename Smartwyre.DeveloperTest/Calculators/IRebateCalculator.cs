using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    public interface IRebateCalculator
    {
        bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
