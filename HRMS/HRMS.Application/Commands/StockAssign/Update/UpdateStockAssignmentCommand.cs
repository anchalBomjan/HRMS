using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Update
{
    public  class UpdateStockAssignmentCommand:IRequest<int>
    {
        public int Id { get; set; }
        public decimal AssignedQuantity { get; set; }

    }
}
