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

namespace HRMS.Application.Queries.employee.GetEmployeeByIdQuery
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
    {
        private readonly  IApplicationDbContext _context;
        public GetEmployeeByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request , CancellationToken ct)
        {
            var employee= await _context.Employees
            .Where(e => e.Id == request.Id)
            .Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.Phonenumber

            }).FirstOrDefaultAsync(ct);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} not found.");
            }

            return employee;
        }


    }
}
