using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Queries;

public class CabinsQueryHandler : RequestHandlerBase, IRequestHandler<CabinsQuery, CabinVm[]>
{
    private readonly ICabinService _cabinService;

    public CabinsQueryHandler(ICabinService cabinService)
    {
        _cabinService = cabinService;
    }

    public async Task<CabinVm[]> Handle(CabinsQuery request, CancellationToken cancellationToken) =>
        await _cabinService.GetAllAsync(true);

    protected override void DisposeCore()
    {
        _cabinService?.Dispose();
    }
}
