using HRMS.Application.Commands.Role.Create;
using HRMS.Application.Commands.Role.Delete;
using HRMS.Application.Commands.Role.Update;
using HRMS.Application.DTOs;
using HRMS.Application.Queries.Role.GetRoleByIdQuery;
using HRMS.Application.Queries.Role.GetRoleQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    //[Authorize(Roles ="User,HR")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<IActionResult> CreateRoleAsync(RoleCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDTO>))]
        public async Task<IActionResult> GetRoleAsync()
        {
             return Ok(await _mediator.Send(new GetRoleQuery()));

        }
        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(List<RoleResponseDTO>))]
        public async Task<IActionResult> GetRoleByIdAsync(string id)
        {
            return Ok(await _mediator.Send(new GetRoleByIdQuery() { RoleId=id }));

        }

        [HttpDelete("Delete/{id}")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<IActionResult> DeleteRoleAsync(string id)
        {
            return Ok(await _mediator.Send(new DeleteRoleCommand() 
            {
                RoleId=id 
            }));
        }

        [HttpPut("Edit/{id}")]
        [ProducesDefaultResponseType(typeof(int))]

        public async Task<IActionResult> EditRole(string id, [FromBody] UpdateRoleCommand command)
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
