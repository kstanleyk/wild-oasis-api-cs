using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public abstract class CustomersCommand : IRequest<CustomersCommandResponse>
{
    public CustomerVm[] Customers { get; set; }
}