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

            //Retrieve assignment with related stock and its type
            var assignment = await _context.StockAssignments
           .Include(a => a.Stock)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);


            if (assignment == null)
                throw new NotFoundException("StockAssignment", request.Id);

            if (assignment.Stock != null && assignment.Stock.Type == StockType.Asset)
            {
                assignment.Stock.Quantity += assignment.AsssignedQuantity;
            }

            _context.StockAssignments.Remove(assignment);

            await _context.SaveChangesAsync(cancellationToken);

            return request.Id;
        }

    }
}
