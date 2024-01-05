using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomersQueryHandler : RequestHandlerBase, IRequestHandler<CustomersQuery, CustomerVm[]>
{
    private readonly ICustomerService _customerService;

    public CustomersQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<CustomerVm[]> Handle(CustomersQuery request, CancellationToken cancellationToken) =>
        await _customerService.GetAllAsync(true);

    protected override void DisposeCore()
    {
        _customerService?.Dispose();
    }
}
