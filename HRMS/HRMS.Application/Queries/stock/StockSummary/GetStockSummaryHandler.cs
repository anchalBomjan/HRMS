using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
            // here we used AsNoTracking because project on DTO with navigation properties stock with StockAssignment
            //Yes, implicitly.
           // Although you don’t explicitly write a .Join(...) or.Include(...), EF Core translates the navigation property access(s.Assignments) into a SQL query with appropriate JOINs behind the scenes.
            //When you access a collection navigation property inside a LINQ expression like .Select(...) (e.g., s.Assignments.Sum(...)), EF Core generates SQL with JOIN or subqueries to fetch related data in one query.
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





        
    
    
