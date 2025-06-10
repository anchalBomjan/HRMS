using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.Update.UpdateUserRoles
{
    public  class UpdateUserRoleCommand:IRequest<int>
    {
        public string userName { get; set; }
        public IList<string> Roles { get; set; }

    }
}
