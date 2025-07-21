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

            var assignments = await _context.StockAssignments
             .AsNoTracking()
             .Include(a => a.Employee)
             .Include(a => a.Stock)
             .OrderByDescending(a => a.AssigmentDate)
             .ToListAsync(cancellationToken); // Execute query first

            // Now project in memory using LINQ to Objects
            return assignments.Select(a => new StockAssignmentDTO
            {
                Id = a.Id,
                EmployeeName = a.Employee != null ? a.Employee.Name : "Unknown",
                StockName = a.Stock != null ? a.Stock.Name : "Deleted",
                StockType = a.Stock?.Type,
                AssignmentDate = a.AssigmentDate,
                AssignedQuantity = a.AsssignedQuantity
            }).ToList();

        }
    }
}
