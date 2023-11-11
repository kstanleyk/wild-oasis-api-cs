using MediatR;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchQuery : IRequest<BranchVm>
{
    public string Tenant { get; set; }
    public string Code { get; set; }
}