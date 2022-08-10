using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using UserMS.Dtos;
using UserMS.Helpers;
using UserMS.Models;
using UserMS.Repositories.Interfaces;
using UserMS.Services;

namespace UserMS_Test.Services
{
    public class UserServiceTest
    {
        UserService target;
        IMapper _mapper;
        Mock<IUserRepository> _mockUserRepository;
        Mock<ILogger<UserService>> _mockLogger;
        public UserServiceTest()
        {
            SetupMapper();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockLogger = new Mock<ILogger<UserService>>();

            target = new UserService(_mockUserRepository.Object, _mapper, _mockLogger.Object);
        }

        private void SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new Profile[] {
                        new MappingProfile()
                    });
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public void GetAllActiveUsers_ShouldReturnIsSuccessTrue_AndReturnListOfUsers_When_ListOfUsersIsFound()
        {
            //Arrange
            var listOfActiveUsers = CreateListOfActiveUsers();

            _mockUserRepository
                .Setup(m => m.GetAllActiveUsers()).Returns(listOfActiveUsers);

            //Act
            var res = target.GetAllActiveUsers();

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
            Assert.AreEqual(res.Data, listOfActiveUsers);
        }

        [Test]
        public void GetAllActiveUsers_ShouldReturnIsSuccessFalse_When_ListOfUsersIsNotFound()
        {
            //Arrange
            var listOfActiveUsers = CreateListOfNotActiveUsers();

            _mockUserRepository
                .Setup(m => m.GetAllActiveUsers()).Returns<List<User>>(null);

            //Act
            var res = target.GetAllActiveUsers();

            //Assert
            Assert.AreEqual(res.IsSuccess, false);
        }

        [Test]
        public void AddUser_ShouldAddUser_When_DataIsOk()
        {
            //Arrange
            var userDto = CreateUserDto();
            var user = CreateUser();

            _mockUserRepository
                .Setup(m => m.AddUser(user));

            //Act
            var res = target.AddUser(userDto);

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
        }

        [Test]
        public void UpdateUserState_ShouldReturnIsSuccessTrue_When_IdExist()
        {
            //Arrange
            var userId = 1;
            var userState = true;

            _mockUserRepository
                .Setup(m => m.UpdateUserState(userId, userState)).Returns(true);

            //Act
            var res = target.UpdateUserState(userId, userState);

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
        }

        [Test]
        public void UpdateUserState_ShouldReturnIsSuccessFalse_When_IdNotExist()
        {
            //Arrange
            var userId = 2;
            var userState = true;

            _mockUserRepository
                .Setup(m => m.UpdateUserState(userId, userState)).Returns(false);

            //Act
            var res = target.UpdateUserState(userId, userState);

            //Assert
            Assert.AreEqual(res.IsSuccess, false);
        }

        private List<User> CreateListOfActiveUsers()
        {
            var listOfUsers = new List<User>();

            listOfUsers.Add(new User { Id = 1, Name = "Puli", Active = true, BirthDate = DateTime.Now });
            listOfUsers.Add(new User { Id = 2, Name = "Bo", Active = true, BirthDate = DateTime.Now });
            listOfUsers.Add(new User { Id = 3, Name = "Poco", Active = true, BirthDate = DateTime.Now });

            return listOfUsers;
        }

        private List<User> CreateListOfNotActiveUsers()
        {
            var listOfUsers = new List<User>();

            listOfUsers.Add(new User { Id = 1, Name = "Sandy", Active = false, BirthDate = DateTime.Now });
            listOfUsers.Add(new User { Id = 2, Name = "Brock", Active = false, BirthDate = DateTime.Now });
            listOfUsers.Add(new User { Id = 3, Name = "Amelie", Active = false, BirthDate = DateTime.Now });

            return listOfUsers;
        }

        private UserDto CreateUserDto()
        {
            return new UserDto() { BirthDate = DateTime.Now, Name = "MetalGear" };
        }

        private User CreateUser()
        {
            return new User() { BirthDate = DateTime.Now, Name = "Orsini" };
        }
    }
}
