using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Queries.User.GetAllUsersDetailsQuery
{
    public  class GetAllUsersDetailsQueryHandler:IRequestHandler<GetAllUsersDetailsQuery ,List<UserDetailsResponseDTO>>
    {
        private readonly IIdentityService _identityService;
        public GetAllUsersDetailsQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<List<UserDetailsResponseDTO>> Handle(GetAllUsersDetailsQuery request,CancellationToken ct)
        {
            var users = await _identityService.GetAllUsersDetailsAsync();

            //fetch raw data from source and map to dto to store
            var userDetails=users.Select(x => new UserDetailsResponseDTO()
            {
                Id=x.id,
                Email=x.email,
                UserName=x.userName,
                FullName=x.fullName,
            }).ToList();

            foreach(var user in userDetails)
            {
                user.Roles = await _identityService.GetUserRolesAsync(user.Id);
            }

            return userDetails;
        }
    }
}
