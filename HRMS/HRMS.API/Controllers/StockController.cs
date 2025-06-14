using HRMS.Application.Commands.stock.Create;
using HRMS.Application.Commands.stock.Delete;
using HRMS.Application.Commands.stock.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IMediator _mediator;
        public StockController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> Create(CreateStockCommand command)
        {
           return  Ok(await _mediator.Send(command));

        }


        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Update(int id, UpdateStockCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<string>> Delete(int id)
        { 
            return Ok(await _mediator.Send(new DeleteStockCommand { Id=id})); 
        }

    }
}
