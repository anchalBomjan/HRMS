using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockQuery
{

    //IEnumerable use for large data 1m+ records(Read-only sequence
    //List use for samall data (mutabale(add/remove items)

    public  class GetAllStocksQuery:IRequest<List<StockResponse>>
    {
    }
}
