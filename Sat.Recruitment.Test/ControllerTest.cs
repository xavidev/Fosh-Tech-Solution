using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Api;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class ControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> fixture;

        public ControllerTest(WebApplicationFactory<Program> fixture)
        {
            this.fixture = fixture;
        }

        protected HttpClient CreateClient()
        {
            return this.fixture.CreateClient();
        }
    }
}