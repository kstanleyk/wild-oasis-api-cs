using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchCountQueryHandler : RequestHandlerBase, IRequestHandler<BranchCountQuery, int>
{
    private readonly IBranchService _branchService;

    public BranchCountQueryHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<int> Handle(BranchCountQuery request, CancellationToken cancellationToken) =>
        await _branchService.GetCount(true);

    protected override void DisposeCore()
    {
        _branchService?.Dispose();
    }
}