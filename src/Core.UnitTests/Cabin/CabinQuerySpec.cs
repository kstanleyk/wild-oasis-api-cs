using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using WildOasis.Application.Cabin;
using WildOasis.Application.Cabin.Queries;
using WildOasis.Domain.Contracts.Service;
using WildOasis.Domain.Vm;
using Xunit;

namespace WildOasis.Application.Test.Cabin
{
    public class CabinQuerySpec
    {
        private readonly ICabinService _cabinService;

        public CabinQuerySpec()
        {
            var persistence = CabinPersistenceMock.GetCabinPersistence();

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            _cabinService = new CabinService(persistence.Object, mapper);
        }

        [Fact]
        public async Task Handle_CabinsQuery_Should_Return_Cabin_Vms()
        {
            //Arrange
            var sut = new CabinsQueryHandler(_cabinService);

            //Act
            var result = await sut.Handle(new CabinsQuery(), CancellationToken.None);


            //Assert
            result.ShouldBeOfType<CabinVm[]>();
            result.Length.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_CabinCountQuery_Should_Return_Cabin_Count()
        {
            //Arrange
            var sut = new CabinCountQueryHandler(_cabinService);

            //Act
            var result = await sut.Handle(new CabinCountQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<int>();
            result.ShouldBe(2);
        }
    }
}