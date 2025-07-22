using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.employee.Update
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, string>
    {

        private readonly IApplicationDbContext _context;
        public UpdateEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public  async Task<string> Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            //if (request.Id <= 0)
            //    throw new BadRequestException("Employee ID must be greater than zero.");
            if (request.Id <= 0)
                throw new BadRequestException("Employee ID must be greater than zero.");



            var employee = await _context.Employees.FindAsync( new object[] { request.Id },ct);
            //if (employee == null) return "Employee not found";
            if (employee == null)
                throw new NotFoundException(nameof(Employee), request.Id);

            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Phonenumber = request.PhoneNumber;

            await _context.SaveChangesAsync(ct);
            return "Employee updated successfully";


        }

        
    }
}
