using HRMS.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Stock.Update
{
    public class UpdateStockCommand:IRequest<string>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public StockType StockType { get; set; }
        public  bool IsDozen {  get; set; }
    }
}
