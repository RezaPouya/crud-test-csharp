using Mc2.CrudTest.Domain.Application.Customers.Commands;
using Mc2.CrudTest.Domain.Application.Customers.Queries;
using Mc2.CrudTest.Domain.Customers.DTOs.OutputDtos;
using Mc2.CrudTest.HttpApi.Host.Filters;
using Mc2.CrudTest.HttpApi.Host.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Mc2.CrudTest.HttpApi.Host.Controllers
{
    [ApiController]
    //[ApiExceptionFilter]
    //[ApiResultFilter]
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
        public async Task<ApiResult> Create(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ApiResult> Update(int id, UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest();

            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
            return Ok();
        }

        [HttpGet("email/{email}")]
        public async Task<CustomerOutputDto> GetByEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email))
                return null;

            var response =  await _mediator.Send(new GetCustomerByEmailQuery(email), cancellationToken);
            return response;
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<CustomerOutputDto>> GetById(int id, CancellationToken cancellationToken)
        {
            if (id == default(int) || id < 0)
                return BadRequest();

            return await _mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<CustomerOutputDto>>> GetAll(CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
        }
    }
}