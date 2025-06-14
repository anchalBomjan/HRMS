using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockByIdQuery
{
    public  class GetStockByIdQuery:IRequest<StockResponse>
    {
        public int Id { get; set; }
    }
}
