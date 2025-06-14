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
        //public async Task<string> Handle(UpdateStockCommand request, CancellationToken ct)
        //{
        //    using var transaction = await _context.Database.BeginTransactionAsync(ct);

        //    try
        //    {
        //        // Lock stock record
        //        var stock = await _context.Stocks
        //            .FromSqlInterpolated($"SELECT * FROM Stocks WITH (UPDLOCK) WHERE Id = {request.Id}")
        //            .FirstOrDefaultAsync(ct);

        //        if (stock == null) return "Stock not found";

        //        // Convert to base units
        //        decimal quantityInUnits = request.IsDozen
        //            ? request.Quantity * 12
        //            : request.Quantity;

        //        // Update all properties
        //        stock.Name = request.Name;
        //        stock.Description = request.Description;
        //        stock.Type = request.Type;
        //        stock.Quantity = quantityInUnits;

        //        await _context.SaveChangesAsync(ct);
        //        await transaction.CommitAsync(ct);

        //        return "Stock updated successfully";
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        await transaction.RollbackAsync(ct);

        //        // Refresh entity state
        //        var entry = ex.Entries.Single();
        //        await entry.ReloadAsync(ct);

        //        return "Stock was modified by another user. Please refresh and try again.";
        //    }
        //    catch (Exception ex)
        //    {
        //        await transaction.RollbackAsync(ct);
        //        return $"Update failed: {ex.Message}";
        //    }
        //}


        public async Task<string> Handle(UpdateStockCommand request, CancellationToken ct)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(ct);

            try
            {
                // Lock stock record
                var stock = await _context.Stocks
                    .FromSqlInterpolated($"SELECT * FROM Stocks WITH (UPDLOCK) WHERE Id = {request.Id}")
                    .FirstOrDefaultAsync(ct);

                if (stock == null) return "Stock not found";

                // Convert to base units
                decimal quantityInUnits = request.IsDozen
                    ? request.Quantity * 12
                    : request.Quantity;

                // Update properties
                stock.Name = request.Name;
                stock.Description = request.Description;
                stock.Type = request.Type;

                // MODIFIED: Additive or absolute quantity update
                if (request.IsAdditive)
                {
                    stock.Quantity += quantityInUnits;  // ADD to existing quantity
                }
                else
                {
                    stock.Quantity = quantityInUnits;   // SET absolute quantity
                }

                await _context.SaveChangesAsync(ct);
                await transaction.CommitAsync(ct);

                return request.IsAdditive
                    ? $"Added {quantityInUnits} units. New quantity: {stock.Quantity}"
                    : "Stock updated successfully";
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await transaction.RollbackAsync(ct);
                var entry = ex.Entries.Single();
                await entry.ReloadAsync(ct);
                return "Stock was modified by another user. Please refresh and try again.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                return $"Update failed: {ex.Message}";
            }
        }


    }
}
