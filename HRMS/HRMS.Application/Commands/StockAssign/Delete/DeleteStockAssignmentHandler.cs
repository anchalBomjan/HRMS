using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.StockAssign.Delete
{
    public  class DeleteStockAssignmentHandler:IRequestHandler<DeleteStockAssignmentCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public DeleteStockAssignmentHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> Handle(
            DeleteStockAssignmentCommand request,
            CancellationToken cancellationToken)
        {
            // 1. Retrieve assignment with related stock and its type
            var assignment = await _context.StockAssignments
                .Include(a => a.Stock)  // Include stock details
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (assignment == null)
                throw new NotFoundException("StockAssignment", request.Id);

            // 2. Conditionally restore quantity based on stock type
            if (assignment.Stock.Type == StockType.Asset)
            {
                // Return quantity to stock for Assets
                assignment.Stock.Quantity += assignment.AsssignedQuantity;
            }
            // For Consumable, we don't restore quantity (it's consumed)

            // 3. Remove assignment
            _context.StockAssignments.Remove(assignment);

            // 4. Save changes
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }

    }
}
