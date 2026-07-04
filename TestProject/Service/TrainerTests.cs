using Gym.BusinessLogic;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.DataAccess.Data.Enums;
using Gym.DataAccess.Models;
using Gym.DataAccess.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicTest.Service
{
    public class TrainerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ITrainerReposatiory> _trainerRepoMock;
        private readonly TrainerService _service;

        public TrainerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _trainerRepoMock = new Mock<ITrainerReposatiory>();

            _unitOfWorkMock.Setup(x => x.Trainers)
                .Returns(_trainerRepoMock.Object);

            _service = new TrainerService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenEmailIsExists()
        {
            //Arrange
            _trainerRepoMock.Setup(t => t.IsEmailExists(It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var model = new TrainerCreateViewModel
            {
                Email = "test@test.com",
            };

            //Act
            var result = await _service.CreateAsync(model, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Email already exists.", result.Error);

            _trainerRepoMock.Verify(
                x => x.AddAsync( It.IsAny<Trainer>(), It.IsAny<CancellationToken>()),
                Times.Never);
            _trainerRepoMock.Verify(
                 x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                 Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnError_WhenPhoneIsExists()
        {
            //Arrange
            _trainerRepoMock.Setup(t => t.IsPhoneExists(It.IsAny<string>(),
                It.IsAny<int?>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var model = new TrainerCreateViewModel
            {
                Phone = "012351532513",
            };

            //Act
            var result = await _service.CreateAsync(model, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Phone number already exists.", result.Error);

            _trainerRepoMock.Verify(
                x => x.AddAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()),
                Times.Never);
            _trainerRepoMock.Verify(
                 x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                 Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateTrainer_WhenDataIsValid()
        {
            // Arrange
            var model = new TrainerCreateViewModel
            {
                Name = "Ahmed",
                Email = "ahmed@test.com",
                Phone = "01000000000",
                DateOfBirth = new DateOnly(2000, 1, 1),
                Gender = Gender.Male,
                BuildingNumber = 10,
                City = "Cairo",
                Street = "Nasr City",
                specialties = new Specialties()
            };

            _trainerRepoMock
                .Setup(x => x.IsEmailExists(model.Email, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            _trainerRepoMock
                .Setup(x => x.IsPhoneExists(model.Phone, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            // Act
            var result = await _service.CreateAsync(model, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);

            _trainerRepoMock.Verify(
                x => x.AddAsync(It.IsAny<Trainer>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _trainerRepoMock.Verify(
                x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllTrainers()
        {
            // Arrange
            var trainers = new List<Trainer>
            {
                new Trainer
                {
                    Id = 1,
                    Name = "Ahmed",
                    Email = "ahmed@test.com",
                    Phone = "01000000000",
                    Specialties = Specialties.Cardio
                },
                new Trainer
                {
                    Id = 2,
                    Name = "Ali",
                    Email = "ali@test.com",
                    Phone = "01111111111",
                    Specialties = Specialties.Yoga
                }
            };

            _trainerRepoMock
                .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(trainers);

            // Act
            var result = (await _service.GetAllAsync(CancellationToken.None)).ToList();

            // Assert
            Assert.Equal(2, result.Count);

            Assert.Equal("Ahmed", result[0].Name);
            Assert.Equal("ahmed@test.com", result[0].Email);
            Assert.Equal("01000000000", result[0].Phone);

            Assert.Equal("Ali", result[1].Name);
            Assert.Equal("ali@test.com", result[1].Email);
            Assert.Equal("01111111111", result[1].Phone);
        }
    }
}
