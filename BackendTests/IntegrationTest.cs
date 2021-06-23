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

            // _factory = new WebApplicationFactory<Startup>()
            //     .WithWebHostBuilder(builder =>
            //         builder.UseSetting("https_port", "5001").UseEnvironment("Testing")
            //     );

            _client = appFactory.CreateClient();
        }
    }
}
