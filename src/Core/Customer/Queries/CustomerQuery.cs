using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomerQuery : IRequest<CustomerVm>
{
    public string Tenant { get; set; }
    public string Code { get; set; }
}