using Azure.Core;
using HRMS.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Application.Commands.User.ResetPassword
{
    public  class ResetPasswordCommandHandler:IRequestHandler<ResetPasswordCommand, bool>
    {

        private readonly IIdentityService _identityService;
        private readonly ILogger<ResetPasswordCommandHandler> _logger;
        //public ResetPasswordCommandHandler(IIdentityService identityService, ILogger<ResetPasswordCommand> logger)
        //{
        //    _identityService = identityService;
        //    _logger = logger;
        //}
        public ResetPasswordCommandHandler(IIdentityService identityService, ILogger<ResetPasswordCommandHandler> logger)
        {
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<bool>Handle(ResetPasswordCommand request , CancellationToken ct)
        {
            try
            {
                var result = await _identityService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
                if (!result)
                {
                    _logger.LogWarning("Failed to reset password for{Email}", request.Email);
                }
                 return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex , "Errors resetting password for {Email}",request.Email);
                return false;
            }
        }



    }
}
