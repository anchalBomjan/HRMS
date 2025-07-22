using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.StockSummary
{
    public class GetStockSummaryHandler : IRequestHandler<GetStockSummaryQuery, List<StockSummaryViewModel>>
    {
        private readonly IApplicationDbContext _context;

        public GetStockSummaryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StockSummaryViewModel>> Handle(GetStockSummaryQuery request, CancellationToken ct)
        {
            var summary = await _context.Stocks
            .AsNoTracking()
            .Select(s => new StockSummaryViewModel
            {
             StockId = s.Id,
             StockName = s.Name,
         
             StockType = s.Type.ToString(),
             TotalQuantity = s.Quantity,
             UsedQuantity = s.Assignments.Sum(a => (decimal?)a.AsssignedQuantity) ?? 0,
             RemainingQuantity = s.Quantity - (s.Assignments.Sum(a => (decimal?)a.AsssignedQuantity) ?? 0)
            })
            .ToListAsync(ct);

            return summary;




        }
    }
}





        
    
    
