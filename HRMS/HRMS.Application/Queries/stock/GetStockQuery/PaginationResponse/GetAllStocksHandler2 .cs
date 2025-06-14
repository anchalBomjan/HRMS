using HRMS.Application.Common.Interfaces;
using HRMS.Application.Common.Pagination;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockQuery.PaginationResponse
{
    public  class GetAllStocksHandler : IRequestHandler<GetAllStocksQuery2, PaginatedResponse<StockResponse>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllStocksHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<PaginatedResponse<StockResponse>> Handle(
           GetAllStocksQuery2 request,
           CancellationToken cancellationToken)
        {
            // Base query without pagination
            var query = _context.Stocks.AsNoTracking();

            // Get total count (before pagination)
            var totalItems = await query.CountAsync(cancellationToken);

            // Apply pagination and execute query
            var items = await query
                .OrderBy(s => s.Name)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new StockResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    StockType = s.Type,
                    Quantity = s.Quantity
                })
                .ToListAsync(cancellationToken);

            return new PaginatedResponse<StockResponse>(
                items,
                totalItems,
                request.Page,
                request.PageSize);
        }
    }
}
