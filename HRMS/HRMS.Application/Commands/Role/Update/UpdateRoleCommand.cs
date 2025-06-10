using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Role.Update
{
    public  class UpdateRoleCommand:IRequest<int>
    {
       
        public string Id { get; set; }
        public string RoleName { get; set; }


    }
}
