using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomersPaginationQueryHandler : RequestHandlerBase, IRequestHandler<CustomersPaginationQuery, CustomerVm[]>
{
    private readonly ICustomerService _customerService;

    public CustomersPaginationQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<CustomerVm[]> Handle(CustomersPaginationQuery request, CancellationToken cancellationToken) =>
        await _customerService.GetAllAsync(true, request.Page, request.Count);

    protected override void DisposeCore()
    {
        _customerService?.Dispose();
    }
}
