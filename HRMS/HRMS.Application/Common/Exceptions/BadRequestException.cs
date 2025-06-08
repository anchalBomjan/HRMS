﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Exceptions
{
    public  class BadRequestException:Exception
    {
        public BadRequestException(string message):base( message){ }
        public BadRequestException(string message, Exception exp):base(message,exp) { }
    }
}
