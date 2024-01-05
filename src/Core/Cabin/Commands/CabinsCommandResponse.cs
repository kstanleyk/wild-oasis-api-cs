using Core.Common.Core;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Commands;

public class CabinsCommandResponse : BaseResponse
{
    public CabinVm[] Data { get; set; }
}