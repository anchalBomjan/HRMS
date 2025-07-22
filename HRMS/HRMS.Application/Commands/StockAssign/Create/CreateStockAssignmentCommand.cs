using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Create
{
    public  class CreateStockAssignmentCommand:IRequest<int>
    {
       
        public int EmployeeId { get; set; }
        public int StockId { get; set; }
        public decimal AssignedQuantity { get; set; }
    }
}
