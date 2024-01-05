using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Commands;

public abstract class CabinCommand : IRequest<CabinCommandResponse>
{
    public CabinVm Cabin { get; set; }
}