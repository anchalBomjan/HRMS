using HRMS.Application.Common.Interfaces;
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

            var employee = await _context.Employees.FindAsync(request.Id);
            if (employee == null) return "Employee not found";

            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Phonenumber = request.PhoneNumber;

            await _context.SaveChangesAsync(ct);
            return "Employee updated successfully";


        }

        
    }
}
