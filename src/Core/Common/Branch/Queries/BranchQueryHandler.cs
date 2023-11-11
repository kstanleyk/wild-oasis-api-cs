using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchQueryHandler : RequestHandlerBase, IRequestHandler<BranchQuery, BranchVm>
{
    private readonly IBranchService _branchService;

    public BranchQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<BranchVm> Handle(BranchQuery request, CancellationToken cancellationToken) =>
        await _branchService.GetAsync(request.Tenant, request.Code);

    protected override void DisposeCore()
    {
        _branchService?.Dispose();
    }
}