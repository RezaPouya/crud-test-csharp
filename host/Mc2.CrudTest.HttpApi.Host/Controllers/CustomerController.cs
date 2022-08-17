using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Application.Customers.Queries;
using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.HttpApi.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private ISender _mediator;

        public CustomerController(ILogger<CustomerController> logger, ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> Update(string email , UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if(email != command.Email)
                return BadRequest();

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{email}")]
        public async Task<IActionResult> Delete(string email, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCustomerCommand(email), cancellationToken);
            return Ok();
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<CustomerOutputDto>> GetByEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            return await _mediator.Send(new GetCustomerByEmailQuery(email), cancellationToken);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CustomerOutputDto>>> GetAll( CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        }
    }
}