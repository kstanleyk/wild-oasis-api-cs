using AutoMapper;
using WildOasis.Domain.Vm;

namespace WildOasis.Application;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entity.Cabin, CabinVm>().ReverseMap();
        CreateMap<Domain.Entity.Customer, CustomerVm>().ReverseMap();
    }
}