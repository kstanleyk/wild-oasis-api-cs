using MediatR;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Queries;

public class BranchesPaginationQuery : IRequest<BranchVm[]>
{
    public int Page { get; set; }
    public int Count { get; set; }
}