using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using WildOasis.Domain.Contracts.Persistence.Common;
using WildOasis.Domain.Contracts.Service.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch;

public class BranchService : ServiceBase<WildOasis.Domain.Entity.Common.Branch, BranchVm>, IBranchService
{
    private readonly IBranchPersistence _branchPersistence;
    private readonly IMapper _mapper;

    public BranchService(IBranchPersistence branchPersistence, IMapper mapper, IDistributedCache cache = null) : 
        base(branchPersistence, mapper, cache)
    {
        _branchPersistence = branchPersistence;
        _mapper = mapper;
    }

    protected override void DisposeCore()
    {
        _branchPersistence?.Dispose();
    }
}