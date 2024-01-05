using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin;

public class CabinService : ServiceBase<Domain.Entity.Cabin, CabinVm>, ICabinService
{
    private readonly ICabinPersistence _cabinPersistence;
    private readonly IMapper _mapper;

    public CabinService(ICabinPersistence cabinPersistence, IMapper mapper, IDistributedCache cache = null) : 
        base(cabinPersistence, mapper, cache)
    {
        _cabinPersistence = cabinPersistence;
        _mapper = mapper;
    }

    protected override void DisposeCore()
    {
        _cabinPersistence?.Dispose();
    }
}