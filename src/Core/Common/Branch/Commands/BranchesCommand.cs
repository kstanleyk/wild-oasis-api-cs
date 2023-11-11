using MediatR;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public abstract class BranchesCommand : IRequest<BranchesCommandResponse>
{
    public string Tenant { get; set; }
    public BranchVm[] Branches { get; set; }
}