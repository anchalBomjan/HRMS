using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Delete
{
    public  class DeleteStockCommand:IRequest<string>
    {

        public int Id { get; set; }
    }
}
