using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockQuery
{
    public  class GetAllStocksQuery:IRequest<List<StockResponse>>
    {
    }
}
