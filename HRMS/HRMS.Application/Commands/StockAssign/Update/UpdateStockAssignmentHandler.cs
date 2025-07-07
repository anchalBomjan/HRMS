using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace HRMS.Application.Commands.StockAssign.Update
{
    public  class UpdateStockAssignmentHandler:IRequestHandler<UpdateStockAssignmentCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public UpdateStockAssignmentHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> Handle(
            UpdateStockAssignmentCommand request,
            CancellationToken cancellationToken)
        {
            //  Retrieve the existing assignment
            var assignment = await _context.StockAssignments
                .Include(a => a.Stock)  // Include stock for quantity adjustment
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (assignment == null)
                throw new NotFoundException("StockAssignment", request.Id);

            // Calculate quantity difference
            var quantityDifference = request.AssignedQuantity - assignment.AsssignedQuantity;

            // Check if stock has sufficient quantity (if increasing assignment)
            if (quantityDifference > 0 && assignment.Stock.Quantity < quantityDifference)
            {
                throw new BadRequestException(
                    $"Insufficient stock. Available: {assignment.Stock.Quantity}, " +
                    $"Required adjustment: {quantityDifference}");
            }

            //  Update assignment quantity
            assignment.AsssignedQuantity = request.AssignedQuantity;

            // Adjust stock quantity
            assignment.Stock.Quantity -= quantityDifference;

           
            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }

    }
}
