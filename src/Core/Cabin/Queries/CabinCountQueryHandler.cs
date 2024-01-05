using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;

namespace WildOasis.Application.Cabin.Queries;

public class CabinCountQueryHandler : RequestHandlerBase, IRequestHandler<CabinCountQuery, int>
{
    private readonly ICabinService _cabinService;

    public CabinCountQueryHandler(ICabinService cabinService)
    {
        _cabinService = cabinService;
    }

    public async Task<int> Handle(CabinCountQuery request, CancellationToken cancellationToken) =>
        await _cabinService.GetCount(true);

    protected override void DisposeCore()
    {
        _cabinService?.Dispose();
    }
}