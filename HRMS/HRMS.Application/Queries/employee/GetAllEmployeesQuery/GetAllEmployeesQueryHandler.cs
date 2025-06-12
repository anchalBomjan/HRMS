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

namespace HRMS.Application.Queries.employee.GetAllEmployeesQuery
{
    public  class GetAllEmployeesQueryHandler:IRequestHandler<GetAllEmployeesQuery,List<EmployeeDTO>>
    {

        private readonly IApplicationDbContext _context;
        public GetAllEmployeesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async  Task<List<EmployeeDTO>> Handle(GetAllEmployeesQuery request, CancellationToken ct)
        {
            var employees = await _context.Employees
           .Select(e => new EmployeeDTO
           {
               Id = e.Id,
               Name = e.Name,
               Email = e.Email,
               PhoneNumber = e.Phonenumber
           })
           .ToListAsync(ct);
            if (employees == null)
            {
                throw new NotFoundException($"Employee  not found.");
            }

            return employees;
        }
    }
}
