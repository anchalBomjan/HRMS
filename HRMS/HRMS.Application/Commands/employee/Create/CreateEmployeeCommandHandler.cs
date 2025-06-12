using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.employee.Create
{
    public class CreateEmployeeCommandHandler:IRequestHandler<CreateEmployeeCommand,int>
    {
        private readonly IApplicationDbContext _context;
        public CreateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
            
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Email = request.Email,
                Phonenumber = request.PhoneNumber
            };

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync(ct);

            return employee.Id;
        }

      

    }
}
