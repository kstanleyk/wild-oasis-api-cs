using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchesQueryHandler : RequestHandlerBase, IRequestHandler<BranchesQuery, BranchVm[]>
{
    private readonly IBranchService _branchService;

    public BranchesQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<BranchVm[]> Handle(BranchesQuery request, CancellationToken cancellationToken) =>
        await _branchService.GetAllAsync(true);

    protected override void DisposeCore()
    {
        _branchService?.Dispose();
    }
}
