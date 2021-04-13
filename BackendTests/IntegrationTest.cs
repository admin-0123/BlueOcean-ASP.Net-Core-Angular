using System.Net.Http;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Virta.Tests
{
    public class IntegrationTest
    {
        protected private HttpClient _client;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>();

            _client = appFactory.CreateClient();
        }
    }
}
