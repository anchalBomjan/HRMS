using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using HRMS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDTO>
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IIdentityService _identityService;
        public AuthCommandHandler(ITokenGenerator tokenGenerator, IIdentityService identityService)
        {
            _identityService = identityService;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponseDTO> Handle(AuthCommand request, CancellationToken ct)
        {
            var result = await _identityService.SigninUserAsync(request.UserName, request.Password);
            if (!result)
            {
                throw new BadRequestException("Invalid username or password");
            }

            /// ********Read this below line code*****
         
            var (userId, fullName, userName, email, roles) = await _identityService.GetUserDetailsAsync(await _identityService.GetUserIdAsync(request.UserName));

            
            string token = _tokenGenerator.GenerateJWTToken((userId: userId, userName: userName, roles: roles));

            return new AuthResponseDTO
            {
                UserId = userId,
                Name = userName,
                Token = token
            };
        }
    }

}
