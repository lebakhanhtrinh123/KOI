﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interface
{
    public interface IVnPayService
    {
        Task<String> CreatePaymentUrlAsync(int orderId);
    }
}
