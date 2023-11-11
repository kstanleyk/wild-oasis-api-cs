using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchesPaginationQueryHandler : RequestHandlerBase, IRequestHandler<BranchesPaginationQuery, BranchVm[]>
{
    private readonly IBranchService _branchService;

    public BranchesPaginationQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<BranchVm[]> Handle(BranchesPaginationQuery request, CancellationToken cancellationToken) =>
        await _branchService.GetAllAsync(true, request.Page, request.Count);

    protected override void DisposeCore()
    {
        _branchService?.Dispose();
    }
}
