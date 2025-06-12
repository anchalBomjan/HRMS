using Azure.Core;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.Queries.employee.GetEmployeeStockAssignmentQuery.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.employee.GetEmployeeStockAssignmentQuery
{
    public  class GetEmployeeStockAssignmentQueryhandler:IRequestHandler<GetEmployeeStockAssignmentQuery,List<EmployeeStockAssignmentViewModel>>
    {

        private readonly IApplicationDbContext _context;
        public GetEmployeeStockAssignmentQueryhandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<List<EmployeeStockAssignmentViewModel>> Handle(GetEmployeeStockAssignmentQuery query, CancellationToken ct)
        {
            var assignments = await _context.StockAssignments
              .Where(sa => sa.EmployeeId == query.EmployeeId)
               .Select(sa => new EmployeeStockAssignmentViewModel
                 {
                   EmployeeId = sa.Employee.Id,
                   EmployeeName = sa.Employee.Name,
                  AssignmentDate = sa.AssigmentDate,
                  AssignedQuantity = sa.AsssignedQuantity,
                  StockName = sa.Stock.Name,
                  StockTotalQuantity = sa.Stock.Quantity
               })
                .ToListAsync(ct);

            return assignments;

        }
    }
}
