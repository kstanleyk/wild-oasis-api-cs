using Core.Common.Core;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public class BranchesCommandResponse : BaseResponse
{
    public BranchVm[] Data { get; set; }
}