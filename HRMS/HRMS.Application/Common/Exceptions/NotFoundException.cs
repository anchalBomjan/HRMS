﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Common.Exceptions
{
    public  class NotFoundException:Exception
    {


        public NotFoundException() : base()
        {

        }
        public NotFoundException(string message) : base(message) { }


        public NotFoundException( string entityName, object key): base($"Entity\"{entityName}\" with key ({key}) was not found")
        {
            
        }
    }
}
