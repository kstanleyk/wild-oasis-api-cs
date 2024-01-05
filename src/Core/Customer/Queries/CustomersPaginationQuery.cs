using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomersPaginationQuery : IRequest<CustomerVm[]>
{
    public int Page { get; set; }
    public int Count { get; set; }
}