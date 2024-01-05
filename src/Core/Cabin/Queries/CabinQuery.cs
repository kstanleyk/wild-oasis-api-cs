using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Queries;

public class CabinQuery : IRequest<CabinVm>
{
    public string Tenant { get; set; }
    public string Code { get; set; }
}