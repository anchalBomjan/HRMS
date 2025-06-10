using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.Update.AssignUsersRole
{
    public  class AssignUserRoleCommandHandler:IRequestHandler<AssignUserRoleCommand,int>
    {
        private readonly IIdentityService _identityService;
        public AssignUserRoleCommandHandler(IIdentityService identityService)
        {
            _identityService=identityService;
            
        }


        public async Task<int> Handle(AssignUserRoleCommand request, CancellationToken ct)
        {
            var result = await _identityService.AssignUserToRole(request.UserName, request.Roles);
            return result ? 1 : 0;
        }
    }
}
