﻿using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Data
{
    public interface IRebateDataStore
    {
        Rebate GetRebate(string identifier);
        void StoreCalculationResult(Rebate rebate, decimal amount);
    }
}
