using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using MediatR;
using WildOasis.Domain.Contracts.Persistence.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public class LoadBranchesCommandHandler : BranchCommandBase,
    IRequestHandler<LoadBranchesCommand, BranchesCommandResponse>
{
    private readonly IBranchPersistence _branchPersistence;
    private readonly IMapper _mapper;

    public LoadBranchesCommandHandler(IBranchPersistence branchPersistence,
        IMapper mapper) : base(mapper)
    {
        _branchPersistence = branchPersistence;
        _mapper = mapper;
    }

    public async Task<BranchesCommandResponse> Handle(LoadBranchesCommand request,
        CancellationToken cancellationToken)
    {
        var sanitizedRequest = Sanitize(request);
        var (response, branches) =
            await ValidateBranchesAsync(sanitizedRequest, cancellationToken);

        if (response.ValidationErrors.Count > 0)
            throw new ValidationException(response.ValidationErrors);

        var result = await _branchPersistence.AddManyAsync(branches);

        if (result.Status != RepositoryActionStatus.Created)
        {
            response.Success = false;
            response.Data = null;

            return response;
        }

        response.Data = _mapper.Map<BranchVm[]>(result.Entity);

        return response;
    }

    protected override void DisposeCore()
    {
        _branchPersistence?.Dispose();
    }
}