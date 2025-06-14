using HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery
{
    public class GetStocksByEmployeeIdQuery : IRequest<List<StockAssignmentViewModel>>
    {
        public int EmployeeId { get; set; }
    }
}
