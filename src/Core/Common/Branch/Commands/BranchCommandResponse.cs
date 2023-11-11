using Core.Common.Core;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public class BranchCommandResponse : BaseResponse
{
    public BranchVm Data { get; set; }
}