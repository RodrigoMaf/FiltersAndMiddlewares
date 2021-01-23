using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Poc.FilterMiddleware.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory;
                    config
                        .SetBasePath(path)
                        .AddCommandLine(args)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddJsonFile("Swagger.json", optional: false)
                        .AddJsonFile("Serilog.json", optional: false)
                        .AddJsonFile("Culture.json", optional: false)
                        .AddEnvironmentVariables("ASPNETCORE_");
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
