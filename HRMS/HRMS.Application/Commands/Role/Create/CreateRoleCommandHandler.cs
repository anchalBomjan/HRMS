using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Role.Create
{
    public  class CreateRoleCommandHandler:IRequestHandler<RoleCreateCommand,int>
    {

        private readonly IIdentityService _identityService;
        public CreateRoleCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
            
        }
        public async Task<int>Handle(RoleCreateCommand request,CancellationToken ct)
        {
            var result = await _identityService.CreateRoleAsync(request.RoleName);
            return result ? 1 : 0;

        }
    }
}
