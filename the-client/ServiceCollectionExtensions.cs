using Microsoft.Extensions.DependencyInjection;
using System;

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
            });

            services.AddSingleton<TheClient>();
        }
    }
}
