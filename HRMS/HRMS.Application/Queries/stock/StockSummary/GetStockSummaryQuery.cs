using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.StockSummary
{
    public  class GetStockSummaryQuery : IRequest<List<StockSummaryViewModel>>
    {
    }
}
