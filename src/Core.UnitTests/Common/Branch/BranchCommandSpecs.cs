using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using Moq;
using Shouldly;
using WildOasis.Application.Common.Branch.Commands;
using WildOasis.Domain.Contracts.Persistence.Common;
using WildOasis.Domain.Vm.Common;
using Xunit;

namespace WildOasis.Application.Test.Common.Branch
{
    public class BranchCommandSpecs
    {
        private readonly Mock<IBranchPersistence> _persistence;
        private readonly IMapper _mapper;

        public BranchCommandSpecs()
        {
            _persistence = BranchPersistenceMock.GetBranchPersistence();

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();

        }

        #region Create functions

        [Fact]
        public async Task Handle_Create_When_Branch_Is_Valid_Should_Add_To_Repo()
        {
            // Arrange
            var vm = new BranchVm
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            };

            var request = new CreateBranchCommand
            {
                Branch = vm
            };

            var sut = new CreateBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);
            var branches = await _persistence.Object.GetAllAsync();

            //Assert
            branches.Length.ShouldBe(3);
            response.Success.ShouldBe(true);
            response.Data.ShouldNotBe(null);
        }

        [Fact]
        public async Task Handle_Create_Should_Clean_White_Spaces_Then_Add_To_Repo()
        {
            // Arrange
            var vm = new BranchVm
            {
                Tenant = $"{new string(' ', 20)}{new string('t', 10)}{new string(' ', 20)}",
                Code = $"{new string(' ', 20)}{new string('c', 5)}{new string(' ', 20)}",
                Description = $"{new string(' ', 20)}{new string('d', 75)}{new string(' ', 20)}",
                BranchName = $"{new string(' ', 20)}{new string('b', 75)}{new string(' ', 20)}",
                BranchShortName = $"{new string(' ', 20)}{new string('b', 35)}{new string(' ', 20)}",
                StationCode = $"{new string(' ', 20)}{new string('s', 10)}{new string(' ', 20)}",
                Address = $"{new string(' ', 20)}{new string('a', 50)}{new string(' ', 20)}",
                Telephone = $"{new string(' ', 20)}{new string('t', 35)}{new string(' ', 20)}",
                Region = $"{new string(' ', 20)}{new string('r', 2)}{new string(' ', 20)}",
                Motto = $"{new string(' ', 20)}{new string('m', 150)}{new string(' ', 20)}",
                HeadOffice = $"{new string(' ', 20)}{new string('h', 2)}{new string(' ', 20)}",
                Employer = $"{new string(' ', 20)}{new string('e', 5)}{new string(' ', 20)}",
                BranchType = $"{new string(' ', 20)}{new string('b', 2)}{new string(' ', 20)}",
                BranchTown = $"{new string(' ', 20)}{new string('b', 35)}{new string(' ', 20)}",
            };

            var request = new CreateBranchCommand
            {
                Branch = vm
            };

            var sut = new CreateBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Success.ShouldBe(true);
            Assert.Equal(new string('t', 10), response.Data.Tenant);
            Assert.Equal(new string('c', 5), response.Data.Code);
            Assert.Equal(new string('d', 75), response.Data.Description);
            Assert.Equal(new string('b', 75), response.Data.BranchName);
            Assert.Equal(new string('b', 35), response.Data.BranchShortName);
            Assert.Equal(new string('s', 10), response.Data.StationCode);
            Assert.Equal(new string('a', 50), response.Data.Address);
            Assert.Equal(new string('t', 35), response.Data.Telephone);
            Assert.Equal(new string('r', 2), response.Data.Region);
            Assert.Equal(new string('m', 150), response.Data.Motto);
            Assert.Equal(new string('h', 2), response.Data.HeadOffice);
            Assert.Equal(new string('e', 5), response.Data.Employer);
            Assert.Equal(new string('b', 2), response.Data.BranchType);
            Assert.Equal(new string('b', 35), response.Data.BranchTown);
        }

        [Fact]
        public async Task Handle_Create_When_Unexpected_Repository_Response_Should_Return_Null_Data()
        {
            // Arrange

            _persistence.Setup(repo => repo.AddAsync(It.IsAny<WildOasis.Domain.Entity.Common.Branch>())).ReturnsAsync(
                (WildOasis.Domain.Entity.Common.Branch _) => new RepositoryActionResult<WildOasis.Domain.Entity.Common.Branch>(null,
                    RepositoryActionStatus.NothingModified));

            var vm = new BranchVm
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            };

            var request = new CreateBranchCommand
            {
                Branch = vm
            };

            var sut = new CreateBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert

            response.Success.ShouldBe(false);
            response.Data.ShouldBe(null);
        }

        [Theory]
        [MemberData(nameof(BranchTestData.TestData), MemberType = typeof(BranchTestData))]
        public async Task Handle_Create_When_Invalid_Data_Should_Generate_Error(BranchVm vm, string expectedMessage)
        {
            // Arrange
            var request = new CreateBranchCommand
            {
                Branch = vm
            };

            var sut = new CreateBranchCommandHandler(_persistence.Object, _mapper);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
            ex.ValidationErrors.Count.ShouldBe(1);
            Assert.Equal(expectedMessage, ex.ValidationErrors.FirstOrDefault());
        }

        #endregion

        #region Edit functions

        [Fact]
        public async Task Handle_Edit_When_Branch_Is_Valid_Should_Update_Repo()
        {
            // Arrange
            var vm = new BranchVm
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            };

            var request = new EditBranchCommand
            {
                Branch = vm
            };

            var sut = new EditBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);
            var branches = await _persistence.Object.GetAllAsync();

            //Assert
            branches.Length.ShouldBe(2);
            response.Success.ShouldBe(true);
            response.Data.ShouldNotBe(null);
        }

        [Fact]
        public async Task Handle_Edit_Should_Clean_White_Spaces_Then_Add_To_Repo()
        {
            // Arrange
            var vm = new BranchVm
            {
                Tenant = $"{new string(' ', 20)}{new string('t', 10)}{new string(' ', 20)}",
                Code = $"{new string(' ', 20)}{new string('c', 5)}{new string(' ', 20)}",
                Description = $"{new string(' ', 20)}{new string('d', 75)}{new string(' ', 20)}",
                BranchName = $"{new string(' ', 20)}{new string('b', 75)}{new string(' ', 20)}",
                BranchShortName = $"{new string(' ', 20)}{new string('b', 35)}{new string(' ', 20)}",
                StationCode = $"{new string(' ', 20)}{new string('s', 10)}{new string(' ', 20)}",
                Address = $"{new string(' ', 20)}{new string('a', 50)}{new string(' ', 20)}",
                Telephone = $"{new string(' ', 20)}{new string('t', 35)}{new string(' ', 20)}",
                Region = $"{new string(' ', 20)}{new string('r', 2)}{new string(' ', 20)}",
                Motto = $"{new string(' ', 20)}{new string('m', 150)}{new string(' ', 20)}",
                HeadOffice = $"{new string(' ', 20)}{new string('h', 2)}{new string(' ', 20)}",
                Employer = $"{new string(' ', 20)}{new string('e', 5)}{new string(' ', 20)}",
                BranchType = $"{new string(' ', 20)}{new string('b', 2)}{new string(' ', 20)}",
                BranchTown = $"{new string(' ', 20)}{new string('b', 35)}{new string(' ', 20)}",
            };

            var request = new EditBranchCommand
            {
                Branch = vm
            };

            var sut = new EditBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Success.ShouldBe(true);
            Assert.Equal(new string('t', 10), response.Data.Tenant);
            Assert.Equal(new string('c', 5), response.Data.Code);
            Assert.Equal(new string('d', 75), response.Data.Description);
            Assert.Equal(new string('b', 75), response.Data.BranchName);
            Assert.Equal(new string('b', 35), response.Data.BranchShortName);
            Assert.Equal(new string('s', 10), response.Data.StationCode);
            Assert.Equal(new string('a', 50), response.Data.Address);
            Assert.Equal(new string('t', 35), response.Data.Telephone);
            Assert.Equal(new string('r', 2), response.Data.Region);
            Assert.Equal(new string('m', 150), response.Data.Motto);
            Assert.Equal(new string('h', 2), response.Data.HeadOffice);
            Assert.Equal(new string('e', 5), response.Data.Employer);
            Assert.Equal(new string('b', 2), response.Data.BranchType);
            Assert.Equal(new string('b', 35), response.Data.BranchTown);
        }

        [Fact]
        public async Task Handle_Edit_When_Unexpected_Repository_Response_Should_Return_Null_Data()
        {
            // Arrange

            _persistence.Setup(repo => repo.EditAsync(It.IsAny<WildOasis.Domain.Entity.Common.Branch>())).ReturnsAsync(
                (WildOasis.Domain.Entity.Common.Branch _) => new RepositoryActionResult<WildOasis.Domain.Entity.Common.Branch>(null,
                    RepositoryActionStatus.NothingModified));

            var vm = new BranchVm
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            };

            var request = new EditBranchCommand
            {
                Branch = vm
            };

            var sut = new EditBranchCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert

            response.Success.ShouldBe(false);
            response.Data.ShouldBe(null);
        }

        [Theory]
        [MemberData(nameof(BranchTestData.TestData), MemberType = typeof(BranchTestData))]
        public async Task Handle_Edit_When_Invalid_Data_Should_Generate_Error(BranchVm vm, string expectedMessage)
        {
            // Arrange
            var request = new EditBranchCommand
            {
                Branch = vm
            };

            var sut = new EditBranchCommandHandler(_persistence.Object, _mapper);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
            ex.ValidationErrors.Count.ShouldBe(1);
            Assert.Equal(expectedMessage, ex.ValidationErrors.FirstOrDefault());
        }

        #endregion
    }
}
