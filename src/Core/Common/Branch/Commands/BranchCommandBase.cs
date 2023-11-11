using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Core;
using Core.Common.Helpers;

namespace WildOasis.Application.Common.Branch.Commands;

public abstract class BranchCommandBase : RequestHandlerBase
{
    private readonly IMapper _mapper;

    protected BranchCommandBase(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected async
        Task<(BranchCommandResponse response, WildOasis.Domain.Entity.Common.Branch
            branch)> ValidateBranchAsync(
        BranchCommand request,
        CancellationToken cancellationToken)
    {
        var response = new BranchCommandResponse();

        var validator = new BranchCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                if (!response.ValidationErrors.Contains(error.ErrorMessage))
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        if (!response.Success) return (response, null);

        var entity = _mapper.Map<WildOasis.Domain.Entity.Common.Branch>(request.Branch);

        return (response, entity);
    }


    protected async Task<(BranchesCommandResponse response, WildOasis.Domain.Entity.Common.Branch[] branches)>
        ValidateBranchesAsync(BranchesCommand request, CancellationToken cancellationToken)
    {
        var response = new BranchesCommandResponse();

        if (string.IsNullOrWhiteSpace(request.Tenant))
        {
            response.Success = false;
            response.ValidationErrors.Add("Invalid tenant code");
            return (response, null);
        }

        if (request.Branches is null)
        {
            response.Success = false;
            response.ValidationErrors.Add("list of Branches can not be empty");
            return (response, null);
        }

        var requestBranches = request.Branches.ToArray();
        foreach (var vm in requestBranches)
        {
            vm.Code = vm.Code.ToTwoChar();
            vm.Tenant = request.Tenant;
            vm.CreatedOn = DateTime.Now.ToUtcDate();
        }

        request.Branches = requestBranches;

        var validator = new BranchesCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors.Where(error =>
                         !response.ValidationErrors.Contains(error.ErrorMessage)))
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (!response.Success) return (response, null);

        var branches = _mapper.Map<WildOasis.Domain.Entity.Common.Branch[]>(request.Branches);

        return (response, branches);
    }
    protected static BranchCommand Sanitize(BranchCommand command)
    {
        if (command.Branch != null)
            command.Branch = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Branch);

        return command;
    }

    protected static BranchesCommand Sanitize(BranchesCommand command)
    {
        if (command.Branches == null) return command;
        for (var i = 0; i <= command.Branches.Length - 1; i++)
            command.Branches[i] = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Branches[i]);

        return command;
    }
}