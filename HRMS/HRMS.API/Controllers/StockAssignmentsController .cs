using HRMS.Application.Commands.StockAssign.Create;
using HRMS.Application.Commands.StockAssign.Delete;
using HRMS.Application.Commands.StockAssign.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
       
        public async Task<ActionResult<int>> UpdateAssignment( int id, [FromBody] UpdateStockAssignmentCommand command)
        {
            // Ensure ID in URL matches ID in command
            if (id != command.Id)
                return BadRequest("ID in route does not match ID in request body");

            var updatedId = await _mediator.Send(command);
            return Ok(updatedId);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> DeleteAssignment(int id)
        {
            return Ok(await _mediator.Send(new DeleteStockAssignmentCommand { Id = id }));
        }

    }
}
