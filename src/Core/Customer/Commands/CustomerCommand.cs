using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public abstract class CustomerCommand : IRequest<CustomerCommandResponse>
{
    public CustomerVm Customer { get; set; }
}