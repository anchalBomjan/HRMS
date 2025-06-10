using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.Update.UpdateUserRoles
{
    public  class UpdateUserRoleCommandHandler:IRequestHandler<UpdateUserRoleCommand,int>
    {
        private  readonly IIdentityService _identityService;
        public UpdateUserRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
            
        }
        public async Task<int> Handle(UpdateUserRoleCommand request, CancellationToken ct)
        {
             var result= await _identityService.UpdateUsersRole(request.userName,request.Roles);

            return result ? 1 : 0;
        }

        

    }
}
