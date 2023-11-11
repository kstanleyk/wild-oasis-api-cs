using AutoMapper;
using WildOasis.Domain.Entity.Common;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Branch, BranchVm>().ReverseMap();
    }
}