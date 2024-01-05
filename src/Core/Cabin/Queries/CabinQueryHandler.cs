using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Queries;

public class CabinQueryHandler : RequestHandlerBase, IRequestHandler<CabinQuery, CabinVm>
{
    private readonly ICabinService _cabinService;

    public CabinQueryHandler(ICabinService cabinService)
    {
        _cabinService = cabinService;
    }

    public async Task<CabinVm> Handle(CabinQuery request, CancellationToken cancellationToken) =>
        await _cabinService.GetAsync(request.Code);

    protected override void DisposeCore()
    {
        _cabinService?.Dispose();
    }
}