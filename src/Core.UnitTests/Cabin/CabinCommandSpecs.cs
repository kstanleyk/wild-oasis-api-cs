using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using Moq;
using Shouldly;
using WildOasis.Application.Cabin.Commands;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Vm;
using Xunit;

namespace WildOasis.Application.Test.Cabin
{
    public class CabinCommandSpecs
    {
        private readonly Mock<ICabinPersistence> _persistence;
        private readonly IMapper _mapper;

        public CabinCommandSpecs()
        {
            _persistence = CabinPersistenceMock.GetCabinPersistence();

            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfiguration.CreateMapper();

        }

        #region Create functions

        [Fact]
        public async Task Handle_Create_When_Cabin_Is_Valid_Should_Add_To_Repo()
        {
            // Arrange
            var vm = new CabinVm
            {
                Description = new string('d', 75),
            };

            var request = new CreateCabinCommand
            {
                Cabin = vm
            };

            var sut = new CreateCabinCommandHandler(_persistence.Object, _mapper);

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
            var vm = new CabinVm
            {
                Description = $"{new string(' ', 20)}{new string('d', 75)}{new string(' ', 20)}",
            };

            var request = new CreateCabinCommand
            {
                Cabin = vm
            };

            var sut = new CreateCabinCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Success.ShouldBe(true);
            Assert.Equal(new string('d', 75), response.Data.Description);
        }

        [Fact]
        public async Task Handle_Create_When_Unexpected_Repository_Response_Should_Return_Null_Data()
        {
            // Arrange

            _persistence.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entity.Cabin>())).ReturnsAsync(
                (Domain.Entity.Cabin _) => new RepositoryActionResult<Domain.Entity.Cabin>(null,
                    RepositoryActionStatus.NothingModified));

            var vm = new CabinVm
            {
                Description = new string('d', 75),
            };

            var request = new CreateCabinCommand
            {
                Cabin = vm
            };

            var sut = new CreateCabinCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert

            response.Success.ShouldBe(false);
            response.Data.ShouldBe(null);
        }

        [Theory]
        [MemberData(nameof(CabinTestData.TestData), MemberType = typeof(CabinTestData))]
        public async Task Handle_Create_When_Invalid_Data_Should_Generate_Error(CabinVm vm, string expectedMessage)
        {
            // Arrange
            var request = new CreateCabinCommand
            {
                Cabin = vm
            };

            var sut = new CreateCabinCommandHandler(_persistence.Object, _mapper);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
            ex.ValidationErrors.Count.ShouldBe(1);
            Assert.Equal(expectedMessage, ex.ValidationErrors.FirstOrDefault());
        }

        #endregion

        #region Edit functions

        [Fact]
        public async Task Handle_Edit_When_Cabin_Is_Valid_Should_Update_Repo()
        {
            // Arrange
            var vm = new CabinVm
            {
                Description = new string('d', 75),
            };

            var request = new EditCabinCommand
            {
                Cabin = vm
            };

            var sut = new EditCabinCommandHandler(_persistence.Object, _mapper);

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
            var vm = new CabinVm
            {
                Description = $"{new string(' ', 20)}{new string('d', 75)}{new string(' ', 20)}",
            };

            var request = new EditCabinCommand
            {
                Cabin = vm
            };

            var sut = new EditCabinCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert
            response.Success.ShouldBe(true);
            Assert.Equal(new string('d', 75), response.Data.Description);
        }

        [Fact]
        public async Task Handle_Edit_When_Unexpected_Repository_Response_Should_Return_Null_Data()
        {
            // Arrange

            _persistence.Setup(repo => repo.EditAsync(It.IsAny<Domain.Entity.Cabin>())).ReturnsAsync(
                (Domain.Entity.Cabin _) => new RepositoryActionResult<Domain.Entity.Cabin>(null,
                    RepositoryActionStatus.NothingModified));

            var vm = new CabinVm
            {
                Description = new string('d', 75),
            };

            var request = new EditCabinCommand
            {
                Cabin = vm
            };

            var sut = new EditCabinCommandHandler(_persistence.Object, _mapper);

            // Act
            var response = await sut.Handle(request, CancellationToken.None);

            //Assert

            response.Success.ShouldBe(false);
            response.Data.ShouldBe(null);
        }

        [Theory]
        [MemberData(nameof(CabinTestData.TestData), MemberType = typeof(CabinTestData))]
        public async Task Handle_Edit_When_Invalid_Data_Should_Generate_Error(CabinVm vm, string expectedMessage)
        {
            // Arrange
            var request = new EditCabinCommand
            {
                Cabin = vm
            };

            var sut = new EditCabinCommandHandler(_persistence.Object, _mapper);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ValidationException>(() => sut.Handle(request, CancellationToken.None));
            ex.ValidationErrors.Count.ShouldBe(1);
            Assert.Equal(expectedMessage, ex.ValidationErrors.FirstOrDefault());
        }

        #endregion
    }
}
