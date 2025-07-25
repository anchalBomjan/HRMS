﻿using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.employee.Delete
{
    public  class DeleteEmployeeCommandHandler:IRequestHandler<DeleteEmployeeCommand,string>
    {
        private readonly IApplicationDbContext _context;
        public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            // Try to find the employee by Id
            // var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, tc);
            var employee = await _context.Employees.FindAsync(new object[] { request.Id }, ct);

          //  var  employee=await _context.Employees.FindAsync(request.Id,tc);


            if (employee == null)
            {
                return $"Employee with ID {request.Id} not found.";
            }

            // Remove employee
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(ct);

            return $"Employee with ID {request.Id} deleted successfully.";

        }
    }
}
