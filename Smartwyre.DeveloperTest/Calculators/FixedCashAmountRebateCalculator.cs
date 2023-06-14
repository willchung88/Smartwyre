using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    public class FixedCashAmountRebateCalculator : IRebateCalculator
    {
        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedCashAmount &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) &&
                   rebate.Amount > 0;
        }

        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;
        }
    }
}
