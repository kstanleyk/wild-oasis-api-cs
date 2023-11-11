using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Shouldly;
using WildOasis.Application.Common.Branch;
using WildOasis.Application.Common.Branch.Queries;
using WildOasis.Domain.Contracts.Service.Common;
using WildOasis.Domain.Vm.Common;
using Xunit;

namespace WildOasis.Application.Test.Common.Branch
{
    public class BranchQuerySpec
    {
        private readonly IBranchService _branchService;

        public BranchQuerySpec()
        {
            var persistence = BranchPersistenceMock.GetBranchPersistence();

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            var mapper = mapperConfiguration.CreateMapper();

            _branchService = new BranchService(persistence.Object, mapper);
        }

        [Fact]
        public async Task Handle_BranchesQuery_Should_Return_Branch_Vms()
        {
            //Arrange
            var sut = new BranchesQueryHandler(_branchService);

            //Act
            var result = await sut.Handle(new BranchesQuery(), CancellationToken.None);


            //Assert
            result.ShouldBeOfType<BranchVm[]>();
            result.Length.ShouldBe(2);
        }

        [Fact]
        public async Task Handle_BranchCountQuery_Should_Return_Branch_Count()
        {
            //Arrange
            var sut = new BranchCountQueryHandler(_branchService);

            //Act
            var result = await sut.Handle(new BranchCountQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<int>();
            result.ShouldBe(2);
        }
    }
}