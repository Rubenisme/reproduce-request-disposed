using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

namespace ClientLib
{
    public static class ServiceCollectionExtensions
    {
        public static void AddTheClient(this IServiceCollection services, TheClientConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            services.AddHttpClient(nameof(TheClient), biz =>
            {
                biz.BaseAddress = config.BaseUrl;
                biz.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddSingleton<TheClient>();
        }
    }
}
