using FluentValidation;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Create
{
    public class CreateStockAssignmentCommandHandler:IRequestHandler<CreateStockAssignmentCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public CreateStockAssignmentCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> Handle(
           CreateStockAssignmentCommand request,
           CancellationToken cancellationToken)
        {
            // Validate command
            if (request.EmployeeId <= 0 || request.StockId <= 0 || request.AssignedQuantity <= 0)
                throw new BadRequestException("Invalid input parameters");

            // Check employee exists
            var employeeExists = await _context.Employees
                .AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);
            if (!employeeExists)
                throw new NotFoundException("Employee", request.EmployeeId);

            // Retrieve stock with concurrency token
            var stock = await _context.Stocks
                .Where(s => s.Id == request.StockId)
                .FirstOrDefaultAsync(cancellationToken);

            if (stock == null)
                throw new NotFoundException("Stock", request.StockId);

            // Check sufficient quantity
            //if (stock.Quantity < request.AssignedQuantity)
            //    throw new BadRequestException(
            //        $"Insufficient stock. Available: {stock.Quantity}, Requested: {request.AssignedQuantity}");

            //if (stock.Quantity < request.AssignedQuantity)
            //    throw new ValidationException("Insufficient stock to assign");
            if (stock.Quantity < request.AssignedQuantity)
                throw new InsufficientStockException("Insufficient stock to assign");




            // Create assignment
            var assignment = new StockAssignment
            {
                EmployeeId = request.EmployeeId,
                StockId = request.StockId,
                AsssignedQuantity = request.AssignedQuantity,
                AssigmentDate = DateTime.UtcNow
            };

            // Update stock quantity
            stock.Quantity -= request.AssignedQuantity;

            // Execute transaction
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await _context.StockAssignments.AddAsync(assignment, cancellationToken);
                _context.Stocks.Update(stock);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                return assignment.Id;
            }
            catch (DbUpdateConcurrencyException)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw new Exception("Stock was updated by another transaction. Please try again.");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

    }
}
