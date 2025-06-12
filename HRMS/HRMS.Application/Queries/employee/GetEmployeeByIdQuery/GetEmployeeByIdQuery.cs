using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.employee.GetEmployeeByIdQuery
{
    public class GetEmployeeByIdQuery:IRequest<EmployeeDTO>
    {
        //public GetEmployeeByIdQuery(int id)
        //{
        //    Id = id;
        //}

        public int  Id { get; set; }  
    }
}
