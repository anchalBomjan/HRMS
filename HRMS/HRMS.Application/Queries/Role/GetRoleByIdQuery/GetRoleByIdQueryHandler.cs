using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.Role.GetRoleByIdQuery
{
    public  class GetRoleByIdQueryHandler:IRequestHandler<GetRoleByIdQuery,RoleResponseDTO>
    {
         private  readonly IIdentityService _identityService;
        public GetRoleByIdQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        
        public async Task<RoleResponseDTO>  Handle(GetRoleByIdQuery query,CancellationToken ct)
        {
            var role = await _identityService.GetRoleByIdAsync(query.RoleId);

            return new RoleResponseDTO()
            {
                Id = role.id,
                RoleName = role.roleName,
            };
        }
    }
}
