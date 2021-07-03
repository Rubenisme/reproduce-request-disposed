using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ClientLib
{
    public class TheClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TheClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Task UpdateMethod(string id, CancellationToken cancellationToken)
        {
            async Task Call(HttpRequestMessage request)
            {
                using (var httpClient = _httpClientFactory.CreateClient(nameof(TheClient)))
                {
                    using (var response = await httpClient.SendAsync(request, cancellationToken))
                    {
                        var content = await response.Content.ReadAsStringAsync(cancellationToken);

                        if (!response.IsSuccessStatusCode)
                        {
                            throw new Exception("Could not read string, content returned was: " + content);
                        }
                    }
                }
            }

            var url = $"api/ReceivingUpdateApi/UpdateOfProperty/{Uri.EscapeDataString(id)}";
            using (var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new StringContent("hello world") })
            {
                return Call(request);
            }
        }
    }
}
