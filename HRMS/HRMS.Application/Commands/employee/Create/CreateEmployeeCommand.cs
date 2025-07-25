﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.employee.Create
{
    public class CreateEmployeeCommand:IRequest<int>
    {
        public string Name {  get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }

    }
}
