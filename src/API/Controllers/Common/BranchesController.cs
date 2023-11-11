using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WildOasis.API.Core;
using WildOasis.Application.Common.Branch.Commands;
using WildOasis.Application.Common.Branch.Queries;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.API.Controllers.Common;

public class BranchesController : ApiControllerBase 
{
    public BranchesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    [HttpGet]
    public async Task<IActionResult> Get () 
    {
        return await GetActionResult(async () =>
        {
            var query = new BranchesQuery();
            var branches = await _mediator.Send(query);
            return Ok(branches);
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BranchVm branch)
    {
        return await GetActionResult(async () =>
        {
            var command = new CreateBranchCommand
            {
                Branch = branch
            };
            var response = await _mediator.Send(command);
            return Ok(response);
        });
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] BranchVm branch)
    {
        return await GetActionResult(async () =>
        {
            var command = new EditBranchCommand
            {
                Branch = branch
            };
            var response = await _mediator.Send(command);

            if (!response.Success && response.ValidationErrors.Count > 0)
            {
                return ReturnValidationProblem(response);
            }
            return Ok(response);
        });
    }

}
