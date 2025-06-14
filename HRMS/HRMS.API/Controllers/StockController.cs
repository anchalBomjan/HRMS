using HRMS.Application.Commands.stock.Create;
using HRMS.Application.Commands.stock.Delete;
using HRMS.Application.Commands.stock.Update;
using HRMS.Application.Common.Pagination;
using HRMS.Application.DTOs;
using HRMS.Application.Queries.stock.GetStockByIdQuery;
using HRMS.Application.Queries.stock.GetStockQuery;
using HRMS.Application.Queries.stock.GetStockQuery.PaginationResponse;
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

        [HttpGet]
        [ProducesResponseType(typeof(List<StockResponse>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StockResponse>>> GetAllStocks()
        {
            return Ok(await _mediator.Send(new GetAllStocksQuery()));
        }
        //for pagination
        //[HttpGet]
        //[ProducesResponseType(typeof(PaginatedResponse<StockResponse>), StatusCodes.Status200OK)]
        //public async Task<ActionResult<PaginatedResponse<StockResponse>>> GetAllStocks(
        //[FromQuery] int page = 1,
        //[FromQuery] int pageSize = 20)
        //{
        //    return Ok(await _mediator.Send(new GetAllStocksQuery2(page, pageSize)));
        //}

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StockResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StockResponse>> GetStockById(int id)
        {
            return Ok(await _mediator.Send(new GetStockByIdQuery { Id=id }));
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
