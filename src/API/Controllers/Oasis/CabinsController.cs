using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WildOasis.API.Core;
using WildOasis.Application.Cabin.Commands;
using WildOasis.Application.Cabin.Queries;
using WildOasis.Domain.Vm;

namespace WildOasis.API.Controllers.Oasis;

public class CabinsController : ApiControllerBase 
{
    public CabinsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<IActionResult> Get () 
    {
        return await GetActionResult(async () =>
        {
            var query = new CabinsQuery();
            var cabins = await _mediator.Send(query);
            return Ok(cabins);
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CabinVm cabin)
    {
        return await GetActionResult(async () =>
        {
            var command = new CreateCabinCommand
            {
                Cabin = cabin
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] CabinVm cabin)
    {
        return await GetActionResult(async () =>
        {
            var command = new EditCabinCommand
            {
                Cabin = cabin
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
    public async Task<IActionResult> Load([FromBody] CabinVm[] cabins)
    {
        return await GetActionResult(async () =>
        {
            var command = new LoadCabinsCommand
            {
                Cabins = cabins
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        });
    }
}
