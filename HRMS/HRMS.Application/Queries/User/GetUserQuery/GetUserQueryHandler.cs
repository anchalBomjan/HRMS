using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetUserQuery
{
    public class GetUserQueryHandler:IRequestHandler<GetUserQuery,List<UserResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        public GetUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService; 
            
        }

        public async Task<List<UserResponseDTO>> Handle(GetUserQuery query,CancellationToken ct)
        {
            var users = await _identityService.GetAllUsersAsync();
            return users.Select(x => new UserResponseDTO
            {
                Id = x.id,
                FullName = x.fullName,
                UserName = x.userName,
                Email = x.email

            }).ToList();


        }
    }
}
