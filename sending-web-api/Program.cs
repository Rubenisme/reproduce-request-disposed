using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace SendingWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(Configuration)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void Configuration(HostBuilderContext hostingContext, IConfigurationBuilder config)
        {
            config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
#if !DEBUG
                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", optional: false)
#endif
                  .AddEnvironmentVariables();
        }
    }
}
