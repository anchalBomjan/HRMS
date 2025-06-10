using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.Role.GetRoleQuery
{
    public  class GetRoleQueryHandler:IRequestHandler<GetRoleQuery,IList<RoleResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        public GetRoleQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IList<RoleResponseDTO>> Handle(GetRoleQuery query,CancellationToken ct)
        {
            var roles = await _identityService.GetRolesAsync();
            return roles.Select(role=> new RoleResponseDTO()
            {
                Id=role.id,
                RoleName=role.roleName
            }).ToList();
        }
    }
}
