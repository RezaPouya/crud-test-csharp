using Mc2.CrudTest.Domain.Application.Customers.Commands;
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
    }
}