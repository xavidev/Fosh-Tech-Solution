using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers.Users;
using Xunit;

namespace Sat.Recruitment.Test.Controllers.Users
{
    public class UsersControllerTests : ControllerTest
    {
        public UsersControllerTests(WebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var response = await GivenPostRequest(CreateUserRequestMother.NotCreated(), "/users/create-user");
            await AssertUserCreated(response);
        }
        
        [Fact]
        public async Task Should_Not_Create_User_When_Already_Exists()
        {
            var response = await GivenPostRequest(CreateUserRequestMother.ExistentUser(), "/users/create-user");
            await AssertUserDuplicated(response);
        }

        [Fact]
        public async Task Should_Not_Create_User_When_Invalid_User_Request()
        {
            var response = await GivenPostRequest(CreateUserRequestMother.Invalid(), "/users/create-user");
            await AssertInvalidUser(response);
        }

        private async Task AssertInvalidUser(HttpResponseMessage response)
        {
            response.IsSuccessStatusCode.Should().BeTrue();
            var data = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
            data.Errors.Should().Contain("is required", AtLeast.Times(4));
            data.IsSuccess.Should().BeFalse();
        }

        private static async Task AssertUserCreated(HttpResponseMessage response)
        {
            response.IsSuccessStatusCode.Should().BeTrue();
            var data = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
            data.Errors.Should().Be("User Created");
            data.IsSuccess.Should().BeTrue();
        }

        private static async Task AssertUserDuplicated(HttpResponseMessage response)
        {
            response.IsSuccessStatusCode.Should().BeTrue();
            var data = await response.Content.ReadFromJsonAsync<CreateUserResponse>();
            data.Errors.Should().Be("The user is duplicated");
            data.IsSuccess.Should().BeFalse();
        }
    }
}