using HRMS.Application.Commands.employee.Create;
using HRMS.Application.Commands.employee.Update;
using HRMS.Application.Queries.employee.GetAllEmployeesQuery;
using HRMS.Application.Queries.employee.GetEmployeeByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Pkcs;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        [HttpPost("Create Employee")]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeCommand command)
        {
              return Ok(await _mediator.Send(command));

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEmployees()
        {
            return Ok(await _mediator.Send(new GetAllEmployeesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>GetEmployee(int id)
        {
            // return Ok(await _mediator.Send(new GetEmployeeByIdQuery(id)));    //if this the you have create constructor

            return Ok(await _mediator.Send(new GetEmployeeByIdQuery { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] UpdateEmployeeCommand command)
        {

            if(id!=command.Id)
            {
                return BadRequest("ID Mismatch");
            }
            var result=await _mediator.Send(command);

            return Ok(result);


        }

    }
}
