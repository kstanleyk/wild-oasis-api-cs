using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer;

public class CustomerService : ServiceBase<Domain.Entity.Customer, CustomerVm>, ICustomerService
{
    private readonly ICustomerPersistence _customerPersistence;
    private readonly IMapper _mapper;

    public CustomerService(ICustomerPersistence customerPersistence, IMapper mapper, IDistributedCache cache = null) : 
        base(customerPersistence, mapper, cache)
    {
        _customerPersistence = customerPersistence;
        _mapper = mapper;
    }

    protected override void DisposeCore()
    {
        _customerPersistence?.Dispose();
    }
}