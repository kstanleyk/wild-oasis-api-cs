using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Queries;

public class CabinsPaginationQueryHandler : RequestHandlerBase, IRequestHandler<CabinsPaginationQuery, CabinVm[]>
{
    private readonly ICabinService _cabinService;

    public CabinsPaginationQueryHandler(ICabinService cabinService)
    {
        _cabinService = cabinService;
    }

    public async Task<CabinVm[]> Handle(CabinsPaginationQuery request, CancellationToken cancellationToken) =>
        await _cabinService.GetAllAsync(true, request.Page, request.Count);

    protected override void DisposeCore()
    {
        _cabinService?.Dispose();
    }
}
