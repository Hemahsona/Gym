using Gym.BusinessLogic.Services;
using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Data.OwnedType;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace TestProject.Service
{
    public class MemberTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMemberRepository> _memberRepoMock;
        private readonly MemberService _service;

        public MemberTests()  
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _memberRepoMock = new Mock<IMemberRepository>();

            //when memberService call unitOfWork => return mockDB
            _unitOfWorkMock.Setup(x => x.Members)
                           .Returns(_memberRepoMock.Object);
            //inject UnitOfWorkMock
            _service = new MemberService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task GetDetailsAsync_ShouldReturnNull_WhenMemberNotFound()
        {
            // Arrange
            _memberRepoMock
                .Setup(r => r.GetWithMemberShipAync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Member)null);

            // Act
            var result = await _service.GetDetailsAsync(1, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetDetailsAsync_ShouldReturnActiveMembership_WhenExists()
        {
            // Arrange
            var member = new Member
            {
                Id = 1,
                Name = "Hassan",
                Email = "test@test.com",
                Phone = "123",
                Gender = Gender.Male,
                Address = new Address
                {
                    BuildingNumber = 10,
                    Street = "Main",
                    City = "Cairo"
                },
                MemberShips = new List<MemberShip>
                {
                    new MemberShip
                    {
                        StartDate = DateTime.Now.AddDays(-10),
                        EndDate = DateTime.Now.AddDays(10),
                        Plan = new Plan { Name = "Gold" }
                    }
                }
            };

            _memberRepoMock
                .Setup(r => r.GetWithMemberShipAync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(member);

            // Act
            var result = await _service.GetDetailsAsync(1, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Gold", result.PlanName);
            Assert.Equal("Hassan", result.Name);
        }

 
    }
}
