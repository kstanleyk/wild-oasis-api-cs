using Core.Common.Contracts;

namespace WildOasis.Domain.Vm;

public class UserBranchVm : IEntityBase
{
    public string UserCode { get; set; }
    public string Branch { get; set; }
}