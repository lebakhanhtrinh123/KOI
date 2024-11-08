﻿using BusinessLayer.Request;
using BusinessLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface ISaltCalculationService
    {
        Task<AmountSaltModel> CalculateSaltAmount(CaculatorSaltRequest request);
    }
}
