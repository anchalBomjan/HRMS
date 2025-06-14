using HRMS.Application.Common.Interfaces;
using HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery.ViewModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery
{
    public class GetStocksByEmployeeIdHandler : IRequestHandler<GetStocksByEmployeeIdQuery, List<StockAssignmentViewModel>>
    {
        private readonly IApplicationDbContext _context;
        public GetStocksByEmployeeIdHandler(IApplicationDbContext context)
        {

            _context = context;
        }
        public async Task<List<StockAssignmentViewModel>> Handle(
        GetStocksByEmployeeIdQuery request,
        CancellationToken cancellationToken)
        {
            return await _context.StockAssignments
                .AsNoTracking()
                .Include(a => a.Stock)
                .Where(a => a.EmployeeId == request.EmployeeId)
                .Select(a => new StockAssignmentViewModel
                {
                    StockId = a.Id,
                    StockName = a.Stock.Name,
                    Description = a.Stock.Description,
                    StockType = a.Stock.Type,
                    AssignedQuantity = a.AsssignedQuantity,
                    AssignmentDate = (DateTime)a.AssigmentDate
                })
                .ToListAsync(cancellationToken);

        }
    }
}
