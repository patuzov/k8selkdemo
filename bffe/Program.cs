using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;


namespace bffe
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseLogging();
                });
    }

    public static class LoggingExtensions
    {
        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder) =>
            webHostBuilder
                //.UseSetting("suppressStatusMessages", "True") // disable startup logs
                .UseSerilog((context, loggerConfiguration) =>
                {
                    loggerConfiguration.Enrich
                        .FromLogContext()
                        .MinimumLevel.Is(LogEventLevel.Verbose)
                        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                        .MinimumLevel.Override("System", LogEventLevel.Warning)
                        .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                        .Enrich.WithProperty("ApplicationName", "bffe");


                    loggerConfiguration.WriteTo.Console(new ElasticsearchJsonFormatter());
                });
    }
}
