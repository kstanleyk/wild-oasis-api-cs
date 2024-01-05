using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Queries;

public class CustomersQuery : IRequest<CustomerVm[]>
{

}
