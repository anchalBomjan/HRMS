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

           //#### this belows line of code shows that use List<T> when you
           //need to modify the collection (Add, Remove, sort)
           // you need to return the result only as alist and nothing else

           employees.Add(new EmployeeDTO
           {
               Id = 0,
               Name = "Test",
               Email = "test@example.com",
               PhoneNumber = "0000000000"
           });
            employees.RemoveAll(e => string.IsNullOrWhiteSpace(e.Email));
            employees.Sort((x, y) => string.Compare(x.Name, y.Name, StringComparison.OrdinalIgnoreCase));




            return employees;
        }
    }
}
