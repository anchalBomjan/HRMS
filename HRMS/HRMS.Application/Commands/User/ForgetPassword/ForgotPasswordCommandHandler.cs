using HRMS.Application.Common.Exceptions;
using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.ForgetPassword
{
    public  class ForgotPasswordCommandHandler:IRequestHandler<ForgotPasswordCommand , string>
    {

        private readonly IIdentityService _identityService;
        private readonly ILogger<ForgotPasswordCommandHandler> _logger;



        public ForgotPasswordCommandHandler(
            IIdentityService identityService,
            ILogger<ForgotPasswordCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<string> Handle(ForgotPasswordCommand request , CancellationToken ct)
        {
            try
            {

                /// GeneratePasswordResetTokenAsync is microsoftIdentity inbuild function
                var token = await _identityService.GeneratePasswordResetTokenAsync(request.Email);
                _logger.LogInformation("Password rest token for {Email}:{Token}", request.Email, token);
                return token;
            }

            catch (NotFoundException)
            {
                _logger.LogWarning("Email not found:{Email}", request.Email);

                // Return empty string on purpose — avoid revealing existence of email
                return string.Empty;

            }

        }

    }
}
