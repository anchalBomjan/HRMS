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
            //For single-entity read - only queries that project to a DTO,
            //.AsNoTracking() isn’t needed because EF Core doesn’t track projected data,
            var employee= await _context.Employees
            .Where(e => e.Id == request.Id)
            .Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.Phonenumber

            }).FirstOrDefaultAsync(ct);
            // Using FirstOrDefault + Select to fetch a single specific record and project it into a DTO directly from the database

            // 'Select' here means projecting the entity data into a Data Transfer Object (DTO),
            // so that only needed fields are fetched from the database instead of the full entity


            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {request.Id} not found.");
            }

            return employee;
        }


    }
}
