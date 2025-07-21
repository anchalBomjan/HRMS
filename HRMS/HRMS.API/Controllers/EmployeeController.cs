using HRMS.Application.Commands.employee.Create;
using HRMS.Application.Commands.employee.Delete;
using HRMS.Application.Commands.employee.Update;
using HRMS.Application.DTOs;
using HRMS.Application.Queries.employee.GetAllEmployeesQuery;
using HRMS.Application.Queries.employee.GetEmployeeByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Pkcs;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User,HR")]

    public class EmployeeController : ControllerBase
    {

        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        [HttpPost("Create Employee")]
        public async Task<ActionResult<int>> CreateEmployee(CreateEmployeeCommand command)
        {
              return Ok(await _mediator.Send(command));

        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()

        {
            return Ok(await _mediator.Send(new GetAllEmployeesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>>GetEmployee(int id)
        {
            // return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));    //if this the you have create constructor

            return Ok(await _mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

      
       
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {
           
            command.Id = id;
            await _mediator.Send(command);
            return Ok(new { message = "Employee updated successfully." });



        }




        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    var message = await _mediator.Send(new DeleteEmployeeCommand { Id = id });
        //    return Ok(new { message   });


        //}
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand { Id = id }));
        }



    }
}
