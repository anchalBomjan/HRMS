using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Update
{
    public class UpdateStockCommandHandler :IRequestHandler<UpdateStockCommand,string>
    {
        private readonly IApplicationDbContext _context;
        public UpdateStockCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        


        public async Task<string> Handle(UpdateStockCommand request, CancellationToken ct)
        {
           
            
        

            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (stock == null)
                return "Stock not found";

            // Convert to base units
            decimal quantityInUnits = request.IsDozen ? request.Quantity * 12 : request.Quantity;

            // Update properties
            stock.Name = request.Name;
            stock.Description = request.Description;
            stock.Type = request.Type;

            // Additive or absolute quantity update
            if (request.IsAdditive)
            {
                stock.Quantity += quantityInUnits;
            }
            else
            {
                stock.Quantity = quantityInUnits;
            }

            // Save changes (EF Core handles transaction internally)
            await _context.SaveChangesAsync(ct);

            return request.IsAdditive
                ? $"Added {quantityInUnits} units. New quantity: {stock.Quantity}"
                : "Stock updated successfully";



        }


    }
}
