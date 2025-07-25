﻿using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Create
{
    public  class CreateStockCommandHandler:IRequestHandler<CreateStockCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public CreateStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> Handle(CreateStockCommand request, CancellationToken ct)
        {
            // Convert to base units
            decimal quantityInUnits = request.IsDozen
                ? request.Quantity * 12
                : request.Quantity;

            var stock = new Stock
            {
                Name = request.Name,
                Description = request.Description,
                Type = request.Type,
                Quantity = quantityInUnits,
            };

            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync(ct);
            return stock.Id;
        }
    }
}
