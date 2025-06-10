using Azure.Core;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetUserDetailsQuery
{
    public class GetUserDetailsQueryHandler:IRequestHandler<GetUserDetailsQuery,UserDetailsResponseDTO>
    {
        private readonly IIdentityService _identityService;
        public GetUserDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService; 
        }
        public async Task<UserDetailsResponseDTO> Handle(GetUserDetailsQuery query,CancellationToken ct)
        {
            
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(query.UserId);

            return new UserDetailsResponseDTO()
            {
                Id = userId,
                FullName = fullName,
                Email = email,
                Roles = roles
            };
        }
    }
}
