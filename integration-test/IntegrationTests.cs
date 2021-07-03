using ClientLib;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using ReceivingWebApi;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace TestProject
{
    public class IntegrationTests
    {
        private const string Id = "0123456789";

        private WebApplicationFactory<Startup> _factory;
        private HttpClient _httpClient;
        private TheClient _theClient;

        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock = new();

        [SetUp]
        public void SetUp()
        {
            _factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(
                builder => builder.UseEnvironment("Development")
                                  .ConfigureAppConfiguration((_, conf) => conf.AddJsonFile("appsettings.json"))
                                  .ConfigureTestServices(ConfigureTestServices));

            var serviceProvider = _factory.Services;
            var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();

            _httpClient = _factory.CreateClient();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            _httpClientFactoryMock
                .Setup(m => m.CreateClient(nameof(TheClient)))
                .Returns(_httpClient);

            _theClient = new TheClient(httpClientFactory);
        }

        private void ConfigureTestServices(IServiceCollection services)
        {
            services.AddSingleton(_httpClientFactoryMock.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _httpClient?.Dispose();
            _factory?.Dispose();
        }

        [Test]
        public async Task TestUpdateOfProperty()
        {
            await _theClient.UpdateMethod(Id, CancellationToken.None);

            // Assert
            // Does not throw, while it should
        }
    }
}
