using MediatR;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Queries;

public class CabinsPaginationQuery : IRequest<CabinVm[]>
{
    public int Page { get; set; }
    public int Count { get; set; }
}