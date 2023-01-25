using System;
using System.Dynamic;

using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Controllers.Users;
using Xunit;
using UsersController = Sat.Recruitment.Api.Controllers.Users.UsersController;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var userController = new UsersController();

            var result = userController.CreateUser(new CreateUserRequest("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")).Result;


            Assert.Equal(true, result.IsSuccess);
            Assert.Equal("User Created", result.Errors);
        }

        [Fact]
        public void Test2()
        {
            var userController = new UsersController();

            var result = userController.CreateUser(new CreateUserRequest("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", "Normal", "124")).Result;


            Assert.Equal(false, result.IsSuccess);
            Assert.Equal("The user is duplicated", result.Errors);
        }
    }
}
