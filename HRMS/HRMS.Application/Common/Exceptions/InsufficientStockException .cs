﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Exceptions
{
    public class InsufficientStockException:Exception
    {
        public InsufficientStockException(string message) : base(message) { }


    }
}
