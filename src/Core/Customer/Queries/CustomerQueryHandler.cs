using System.Threading;
using System.Threading.Tasks;
using MediatR;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomerQueryHandler : RequestHandlerBase, IRequestHandler<CustomerQuery, CustomerVm>
{
    private readonly ICustomerService _customerService;

    public CustomerQueryHandler(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task<CustomerVm> Handle(CustomerQuery request, CancellationToken cancellationToken) =>
        await _customerService.GetAsync(request.Code);

    protected override void DisposeCore()
    {
        _customerService?.Dispose();
    }
}