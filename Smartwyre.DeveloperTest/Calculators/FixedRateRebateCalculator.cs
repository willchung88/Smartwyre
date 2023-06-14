﻿using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Calculators
{
    public class FixedRateRebateCalculator : IRebateCalculator
    {
        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Incentive == IncentiveType.FixedRateRebate &&
                   product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) &&
                   rebate.Percentage > 0 &&
                   product.Price > 0 &&
                   request.Volume > 0;
        }

        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }
    }
}
