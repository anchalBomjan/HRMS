﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Delete
{
    public  class DeleteStockAssignmentCommand:IRequest<int>
    {

        public int Id { get; set; }
    }
}
