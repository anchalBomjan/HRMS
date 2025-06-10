using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetUserDetailsByUserNameQuery
{
    public  class GetUserdetailsByUserNameQueryHandler:IRequestHandler<GetUserDetailsByUserNameQuery,UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;
        public GetUserdetailsByUserNameQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsByUserNameQuery query, CancellationToken ct)
        {
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsByUserNameAsync(query.UserName);
            return new UserDetailsResponseDTO()
            {
                Id = userId,
                FullName = fullName,
                UserName = userName,
                Email = email,
                Roles = roles
            };
        }

    }
}
