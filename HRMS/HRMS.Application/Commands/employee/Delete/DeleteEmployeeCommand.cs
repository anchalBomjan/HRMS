using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.employee.Delete
{
    public  class DeleteEmployeeCommand:IRequest<string>
    {
        public string Id { get; set; }  
    }
}
