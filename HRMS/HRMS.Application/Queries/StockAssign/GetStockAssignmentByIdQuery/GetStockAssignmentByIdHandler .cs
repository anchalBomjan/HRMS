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

namespace HRMS.Application.Queries.StockAssign.GetStockAssignmentByIdQuery
{
    public  class GetStockAssignmentByIdHandler:IRequestHandler<GetStockAssignmentByIdQuery ,StockAssignmentDTO>
    {
        private readonly IApplicationDbContext _context;
        public GetStockAssignmentByIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<StockAssignmentDTO> Handle(
          GetStockAssignmentByIdQuery request,
          CancellationToken cancellationToken)
        {
            var assignment = await _context.StockAssignments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.Stock)
                .Where(a => a.Id == request.Id)
                .Select(a => new StockAssignmentDTO
                {
                    Id = a.Id,
                    EmployeeName = a.Employee.Name,
                    StockName = a.Stock.Name,
                    StockType = a.Stock.Type, // Include stock type
                    AssignmentDate = a.AssigmentDate,
                    AssignedQuantity = a.AsssignedQuantity
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (assignment == null)
                throw new NotFoundException("StockAssignment", request.Id);

            return assignment;
        }
    }
}
