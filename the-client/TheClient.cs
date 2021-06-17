using Contract;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private static HttpContent GetContent<T>(T t)
        {
            return new StringContent(JsonSerializer.Serialize(t), Encoding.UTF8, "application/json");
        }

        public Task UpdateMethod(string id, UpdateRequest updateRequest, CancellationToken cancellationToken)
        {
            var url = $"api/ReceivingUpdateApi/UpdateOfProperty/{Uri.EscapeDataString(id)}";
            using (var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = GetContent(updateRequest) })
            {
                return Call(request, cancellationToken);
            }
        }

        private async Task Call(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using (var httpClient = _httpClientFactory.CreateClient(nameof(TheClient)))
            {
                using (var response = await httpClient.SendAsync(request, cancellationToken))
                {
                    var content = await response.Content.ReadAsStringAsync(cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException(response.StatusCode, content);
                    }
                }
            }
        }
    }
}
