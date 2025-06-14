using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.stock.GetStockByIdQuery
{
    public  class GetStockByIdHandler:IRequestHandler<GetStockByIdQuery,StockResponse>
    {
        private readonly IApplicationDbContext _context;
        public GetStockByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<StockResponse> Handle(
            GetStockByIdQuery request,
            CancellationToken cancellationToken)
        {
            var stock = await _context.Stocks
                .AsNoTracking()
                .Where(s => s.Id == request.Id)
                .Select(s => new StockResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    StockType = s.Type,               // StockType enum
                    Quantity = s.Quantity
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (stock == null)
            {
                throw new NotFoundException("Stock", request.Id);
            }

            return stock;
        }

    }
}
