using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WildOasis.API.Core;
using WildOasis.Application.Customer.Commands;
using WildOasis.Application.Customer.Queries;
using WildOasis.Domain.Vm;

namespace WildOasis.API.Controllers.Oasis;

public class CustomersController : ApiControllerBase 
{
    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<IActionResult> Get () 
    {
        return await GetActionResult(async () =>
        {
            var query = new CustomersQuery();
            var customers = await _mediator.Send(query);
            return Ok(customers);
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CustomerVm customer)
    {
        return await GetActionResult(async () =>
        {
            var command = new CreateCustomerCommand
            {
                Customer = customer
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CustomerVm customer)
    {
        return await GetActionResult(async () =>
        {
            var command = new EditCustomerCommand
            {
                Customer = customer
            };
            var response = await _mediator.Send(command);

            if (!response.Success && response.ValidationErrors.Count > 0)
            {
                return ReturnValidationProblem(response);
            }
            return Ok(response);
        });
    }

    [HttpPost]
    [Route("load")]
    public async Task<IActionResult> Load([FromBody] CustomerVm[] customers)
    {
        return await GetActionResult(async () =>
        {
            var command = new LoadCustomersCommand
            {
                Customers = customers
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        });
    }
}
