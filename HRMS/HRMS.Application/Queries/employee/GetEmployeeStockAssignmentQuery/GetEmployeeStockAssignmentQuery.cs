using HRMS.Application.DTOs;
using HRMS.Application.Queries.employee.GetEmployeeStockAssignmentQuery.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.employee.GetEmployeeStockAssignmentQuery
{
    public  class GetEmployeeStockAssignmentQuery:IRequest<List<EmployeeStockAssignmentViewModel>>
    {


        public int EmployeeId { get; set; }



    }
}
