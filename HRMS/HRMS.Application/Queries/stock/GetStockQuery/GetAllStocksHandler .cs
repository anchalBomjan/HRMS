using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockQuery
{
    public  class GetAllStocksHandler:IRequestHandler<GetAllStocksQuery,List<StockResponse>>
    {
        private  readonly IApplicationDbContext _context;
        public GetAllStocksHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<List<StockResponse>> Handle( 
           GetAllStocksQuery request,
           CancellationToken ct)
        {
            return await _context.Stocks
                .AsNoTracking()
                .OrderBy(s => s.Name)
                .Select(s => new StockResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    StockType = s.Type,
                    Quantity = s.Quantity
                })
                .ToListAsync(ct);  // Materialize to List
        }
    }
}
