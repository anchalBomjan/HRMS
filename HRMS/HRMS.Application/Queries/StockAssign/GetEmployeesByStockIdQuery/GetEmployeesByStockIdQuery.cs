using HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery
{
    public class GetEmployeesByStockIdQuery : IRequest<List<EmployeeAssignmentViewModel>>
    {
        public int StockId { get; set; }


    }
}
