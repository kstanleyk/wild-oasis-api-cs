using System.IO;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WildOasis.API.Core;

namespace WildOasis.API;

public static class Program
{
    public static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddCommandLine(args)
            .Build();

        var host = Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseConfiguration(config)
                    .UseIISIntegration()
                    .UseWebRoot("wwwroot")
                    .UseStartup<Startup>();

                var protocolSettings = new WebProtocolSettings();
                config.Bind("WebProtocolSettings", protocolSettings);

                webBuilder.UseUrls($"{protocolSettings.Url}:{protocolSettings.Port}");

            }).Build();

        host.Run();
    }
}