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

            // Validate input
            if (request.EmployeeId <= 0 || request.StockId <= 0 || request.AssignedQuantity <= 0)
                throw new BadRequestException("Invalid input parameters");

            // Check if employee exists
            // Returns true or false if any match exists  so we used AnyAsync	
            bool employeeExists = await _context.Employees.AnyAsync(e => e.Id == request.EmployeeId, cancellationToken);
            if (!employeeExists)
                throw new NotFoundException("Employee", request.EmployeeId);

            // Get stock
            // var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == request.StockId, cancellationToken);
            var stock = await _context.Stocks.FindAsync(new object[] { request.StockId }, cancellationToken);

            if (stock == null)
                throw new NotFoundException("Stock", request.StockId);

            if (stock.Quantity < request.AssignedQuantity)
                throw new InsufficientStockException("Insufficient stock to assign");

            // Create new assignment
            var assignment = new StockAssignment
            {
                EmployeeId = request.EmployeeId,
                StockId = request.StockId,
                AsssignedQuantity = request.AssignedQuantity,
                AssigmentDate = DateTime.UtcNow
            };

            // Update stock quantity
            stock.Quantity -= request.AssignedQuantity;
            // doing to operation so we do as below
            await _context.StockAssignments.AddAsync(assignment, cancellationToken);
            _context.Stocks.Update(stock);

            await _context.SaveChangesAsync(cancellationToken);

            return assignment.Id;

        }

    }
}
