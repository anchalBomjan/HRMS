using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetAllStockAssignmentsQuery
{
    public  class GetAllStockAssignmentsHandler : IRequestHandler<GetAllStockAssignmentsQuery, List<StockAssignmentDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllStockAssignmentsHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }
        public async Task<List<StockAssignmentDTO>> Handle(
            GetAllStockAssignmentsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.StockAssignments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Include(a => a.Stock)
                .OrderByDescending(a => a.AssigmentDate)
                .Select(a => new StockAssignmentDTO
                {
                    Id = a.Id,
                    EmployeeName = a.Employee.Name,
                    StockName = a.Stock.Name,
                    StockType = a.Stock.Type, // Include stock type
                    AssignmentDate = a.AssigmentDate,
                    AssignedQuantity = a.AsssignedQuantity
                })
                .ToListAsync(cancellationToken);
        }
    }
}
