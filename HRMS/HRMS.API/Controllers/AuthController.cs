using HRMS.Application.Commands.Auth;
using HRMS.Application.Commands.User.Create;
using HRMS.Application.Commands.User.ForgetPassword;
using HRMS.Application.Commands.User.ResetPassword;
using HRMS.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        [AllowAnonymous]

        public async Task<ActionResult> CreatUser(CreateUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }  




        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDTO>> Login([FromBody] AuthCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("forget-Password")]
        [AllowAnonymous]

        public async Task<ActionResult<string>> ForgotPasswords([FromBody] ForgotPasswordCommand request)
        {
            //var token= await _mediator.Send(request);

            var token = await _mediator.Send(new ForgotPasswordCommand(request.Email));     // *** for this create constructor in forgetPasswordCommand  *****
            if(string.IsNullOrEmpty(token))
            {
                return Ok(new { Message = "If the email exists , a rest token has been sent." });

            }
             return Ok(new {Token=token});
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]

        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordCommand request)
        {
            var success = await _mediator.Send(new ResetPasswordCommand
            {
                Email=request.Email,
                Token=request.Token,
                NewPassword=request.NewPassword,

            });
            return success
               ? Ok(new { Message = "Password reset successful" })
               : BadRequest(new { Message = "Password reset failed." });
        }
    }
}
