using Core.Common.Core;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public class CustomersCommandResponse : BaseResponse
{
    public CustomerVm[] Data { get; set; }
}