using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Role.Delete
{
    public  class DeleteRoleCommand:IRequest<int>
    {
        public string RoleId { get; set; }

       
    }
}
