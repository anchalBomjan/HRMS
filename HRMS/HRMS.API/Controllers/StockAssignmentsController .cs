using HRMS.Application.Commands.StockAssign.Create;
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


    }
}
