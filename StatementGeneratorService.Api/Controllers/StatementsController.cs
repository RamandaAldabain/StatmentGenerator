using MediatR;
using Microsoft.AspNetCore.Mvc;
using StatementGeneratorService.Api.Models.Requests;
using StatementGeneratorService.Application.Features.Statements.Commands;
using StatementGeneratorService.Application.Features.Statements.Queries.GetStatement;

namespace StatementGeneratorService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatementsController : ControllerBase
    {
        private readonly IMediator _mediator;


        public StatementsController(IMediator mediator)
        {
            _mediator = mediator;
        }



        // Generate monthly statement
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateStatement(GenerateStatementRequest request)
        {
            if (request == null)
                return BadRequest("Request body is required.");

            if (string.IsNullOrWhiteSpace(request.CustomerId))
                return BadRequest("CustomerId is required.");

            if (!int.TryParse(request.CustomerId, out var customerId))
                return BadRequest("CustomerId is not a valid int.");

            var command = new GenerateStatementCommand(customerId,request.Month,request.Year);

            var result = await _mediator.Send(command);

            return Ok(result);
        }



        //// Get statement by customer and month
        [HttpGet]
        public async Task<IActionResult> GetStatement(int customerId,int month,int year)
        {

            var query = new GetStatementQuery(customerId,month,year);


            var result = await _mediator.Send(query);


            if (result == null)
                return NotFound("Statement not found");


            return Ok(result);
        }
    }
}