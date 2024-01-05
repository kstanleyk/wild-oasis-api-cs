using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Commands;

public abstract class CabinsCommand : IRequest<CabinsCommandResponse>
{
    public CabinVm[] Cabins { get; set; }
}