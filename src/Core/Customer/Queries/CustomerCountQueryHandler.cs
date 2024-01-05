using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;

namespace WildOasis.Application.Customer.Queries;

public class CustomerCountQueryHandler : RequestHandlerBase, IRequestHandler<CustomerCountQuery, int>
{
    private readonly ICustomerService _customerService;

    public CustomerCountQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<int> Handle(CustomerCountQuery request, CancellationToken cancellationToken) =>
        await _customerService.GetCount(true);

    protected override void DisposeCore()
    {
        _customerService?.Dispose();
    }
}