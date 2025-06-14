using HRMS.Application.Common.Interfaces;
using HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery
{
    public class GetEmployeesByStockIdHandler : IRequestHandler<GetEmployeesByStockIdQuery, List<EmployeeAssignmentViewModel>>
    {
        private readonly IApplicationDbContext _context;
        public GetEmployeesByStockIdHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeAssignmentViewModel>> Handle(
       GetEmployeesByStockIdQuery request,
       CancellationToken cancellationToken)
        {
            return await _context.StockAssignments
                .AsNoTracking()
                .Include(a => a.Employee)
                .Where(a => a.StockId == request.StockId)
                .Select(a => new EmployeeAssignmentViewModel
                {
                    EmployeeId = a.EmployeeId,
                    Name = a.Employee.Name,
                    StockName = a.Stock.Name,
                    Description = a.Stock.Description,
                    StockType = a.Stock.Type,
                    Email = a.Employee.Email,
                    AssignedQuantity = a.AsssignedQuantity,
                    AssignmentDate = (DateTime)a.AssigmentDate
                })
                .ToListAsync(cancellationToken);

        }
    }
}
