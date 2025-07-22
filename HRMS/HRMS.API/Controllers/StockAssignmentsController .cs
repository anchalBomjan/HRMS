using HRMS.Application.Commands.StockAssign.Create;
using HRMS.Application.Commands.StockAssign.Delete;
using HRMS.Application.Commands.StockAssign.Update;
using HRMS.Application.DTOs;
using HRMS.Application.Queries.StockAssign.GetAllStockAssignmentsQuery;
using HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery.ViewModel;
using HRMS.Application.Queries.StockAssign.GetEmployeesByStockIdQuery;
using HRMS.Application.Queries.StockAssign.GetStockAssignmentByIdQuery;
using HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery.ViewModel;
using HRMS.Application.Queries.StockAssign.GetStocksByEmployeeIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User,HR")]

    public class StockAssignmentsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public StockAssignmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> AssignStock(CreateStockAssignmentCommand command)
        {
             return  Ok(await _mediator .Send(command));

        }

        [HttpPut("{id}")]
       
        public async Task<ActionResult<int>> UpdateAssignment( int id, [FromBody] UpdateStockAssignmentCommand command)
        {
            // Ensure ID in URL matches ID in command
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in request body");

            var updatedId = await _mediator.Send(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAssignment(int id)
        {
            return Ok(await _mediator.Send(new DeleteStockAssignmentCommand { Id = id }));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StockAssignmentDTO>> GetAssignmentById(int id)
        {
            return Ok(await _mediator.Send(new GetStockAssignmentByIdQuery { Id=id }));
        }

        [HttpGet]
        public async Task<ActionResult<List<StockAssignmentDTO>>> GetAllAssignments()
        {
            return Ok(await _mediator.Send(new GetAllStockAssignmentsQuery()));
        }



        // Get employees assigned to a specific stock item
        [HttpGet("stock/{stockId}/employees")]
        public async Task<ActionResult<List<EmployeeAssignmentViewModel>>> GetEmployeesByStockId(int stockId)
        {
            return Ok(await _mediator.Send(new GetEmployeesByStockIdQuery { StockId = stockId }));
        }

        // Get stocks assigned to a specific employee
        [HttpGet("employee/{employeeId}/stocks")]
        public async Task<ActionResult<List<StockAssignmentViewModel>>> GetStocksByEmployeeId(int employeeId)
        {
            return Ok(await _mediator.Send(new GetStocksByEmployeeIdQuery{ EmployeeId = employeeId }));
        }
    }
}
