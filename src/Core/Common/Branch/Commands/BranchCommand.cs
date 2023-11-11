using MediatR;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public abstract class BranchCommand : IRequest<BranchCommandResponse>
{
    public BranchVm Branch { get; set; }
}