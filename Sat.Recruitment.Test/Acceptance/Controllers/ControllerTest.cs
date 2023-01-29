using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models.Users;
using Sat.Recruitment.Api.Persistence;
using Xunit;

namespace Sat.Recruitment.Test.Acceptance.Controllers
{
    public class ControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> fixture;

        protected ControllerTest(WebApplicationFactory<Program> fixture)
        {
            this.fixture = fixture;
        }

        private HttpClient CreateClient()
        {
            return this.fixture.CreateClient();
        }

        protected async Task<HttpResponseMessage> GivenPostRequest<T>(T request, string endpoint)
        {
            HttpClient client = CreateClient();

            var response = await client.PostAsync(
                endpoint,
                JsonContent.Create(request)
            );

            return response;
        }

        protected IUserRepository GetUserRepository()
        {
            using var scope = fixture.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            return scope.ServiceProvider.GetRequiredService<IUserRepository>();
        }
    }
}