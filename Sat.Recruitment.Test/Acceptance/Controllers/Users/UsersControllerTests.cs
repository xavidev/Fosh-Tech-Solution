using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Controllers.Users;
using Xunit;

namespace Sat.Recruitment.Test.Acceptance.Controllers.Users
{
    public class UsersControllerTests : ControllerTest
    {
        public UsersControllerTests(WebApplicationFactory<Program> fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Should_Create_User()
        {
            var mike = CreateUserRequestMother.Mike();
            var response = await GivenPostRequest(mike, "/users/create-user");
            await AssertUserCreated(response);
            await GetUserRepository().Delete(mike.Email);
        }

        [Fact]
        public async Task Should_Not_Create_User_When_Already_Exists()
        {
            var response = await GivenPostRequest(CreateUserRequestMother.Agustina(), "/users/create-user");
            await AssertUserDuplicated(response);
        }

        [Fact]
        public async Task Should_Not_Create_User_When_Invalid_User_Request()
        {
            var response = await GivenPostRequest(CreateUserRequestMother.Invalid(), "/users/create-user");
            AssertInvalidRequest(response);
        }

        private static void AssertInvalidRequest(HttpResponseMessage response)
        {
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
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