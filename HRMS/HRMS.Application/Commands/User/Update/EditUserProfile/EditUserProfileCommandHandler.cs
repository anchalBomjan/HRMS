using HRMS.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.Update.EditUserProfile
{
    public  class EditUserProfileCommandHandler:IRequestHandler<EditUserProfileCommand,int>
    {
        private readonly IIdentityService _identityService;
        public EditUserProfileCommandHandler( IIdentityService identityService)
        {

            _identityService = identityService;
            
        }

        public async Task<int>Handle(EditUserProfileCommand request , CancellationToken ct)
        {
            var result = await _identityService.UpdateUserProfile(request.Id, request.FullName, request.Email, request.Roles);
            return result ? 1 : 0;

        }
    }
}
