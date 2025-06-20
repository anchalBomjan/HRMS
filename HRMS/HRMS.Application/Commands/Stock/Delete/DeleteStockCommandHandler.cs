﻿using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.stock.Delete
{
    public  class DeleteStockCommandHandler:IRequestHandler<DeleteStockCommand,string>
    {
        private readonly IApplicationDbContext _context;
        public DeleteStockCommandHandler(IApplicationDbContext conext)
        {

            _context = conext;
            
        }

        public async Task<string> Handle(DeleteStockCommand request, CancellationToken ct)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(ct);

            try
            {
                var stock = await _context.Stocks
                    .FromSqlInterpolated($"SELECT * FROM Stocks WITH (UPDLOCK) WHERE Id = {request.Id}")
                    .FirstOrDefaultAsync(ct);

                if (stock == null) return "Stock not found";

                // Delete stock (assignments will have StockId set to NULL automatically)
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                return "Stock deleted successfully. Assignments preserved with NULL reference.";
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync(ct);
                return "Stock was modified by another user. Please refresh and try again.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                return $"Deletion failed: {ex.Message}";
            }
        }

    }
}
