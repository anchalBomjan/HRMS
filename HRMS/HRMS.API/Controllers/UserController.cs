using HRMS.Application.Commands.User.Create;
using HRMS.Application.Commands.User.Delete;
using HRMS.Application.Commands.User.Update.AssignUsersRole;
using HRMS.Application.Commands.User.Update.EditUserProfile;
using HRMS.Application.Commands.User.Update.UpdateUserRoles;
using HRMS.Application.Common.Exceptions;
using HRMS.Application.DTOs;
using HRMS.Application.Queries.User.GetAllUsersDetailsQuery;
using HRMS.Application.Queries.User.GetUserDetailsByUserNameQuery;
using HRMS.Application.Queries.User.GetUserDetailsQuery;
using HRMS.Application.Queries.User.GetUserQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
   // [Authorize(Roles ="User, HR")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;
        public UserController( IMediator mediator)
        {
            _mediator = mediator;
        }

    

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<UserResponseDTO>))]
        public async Task<IActionResult> GetAllUserAsync()
        {

            return Ok(await _mediator.Send(new GetUserQuery()));
        }

        [HttpDelete("Delete/{id}")]

        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult>DeleteUser(string userId)
        {
            var result= await _mediator.Send(new DeleteUserCommand() { Id= userId });
            return Ok(result);
        }

        [HttpGet("GetUserDetails/{userId}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetails(string userId)
        {
            try
            {
                var result = await _mediator.Send(new GetUserDetailsQuery { UserId = userId });
                return Ok(result);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new
                {
                     error="User not found",
                     message=ex.Message

                });
            }
        }
        [HttpGet("GetUserDetailsByUserName/{userName}")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetUserDetailsByUserName(string userName)
        {
            var result = await _mediator.Send(new GetUserDetailsByUserNameQuery() { UserName = userName });
            return Ok(result);

        }
        [HttpPost("AssignRoles")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<IActionResult> AssignRoles(AssignUserRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        

        [HttpPut("EditUserRole")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> EditUserRoles(UpdateUserRoleCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("GetAllUserDetails")]
        [ProducesDefaultResponseType(typeof(UserDetailsResponseDTO))]
        public async Task<IActionResult> GetAllUserDetails()
        {
            var result = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(result);
        }

        [HttpPut("EditUserProfile/{id}")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<IActionResult> EditUserProfile(string id, [FromBody ] EditUserProfileCommand command)
        {
            if(id==command.Id)
            {
                var result = await _mediator.Send(command);
                return Ok(result);  
            }
            else
            {
                return BadRequest();
            }
        }




    }
}
