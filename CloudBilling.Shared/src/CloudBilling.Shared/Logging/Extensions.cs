using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Events;
using System;
using System.Reflection;

namespace CloudBilling.Shared.Logging
{
    public static class Extensions
    {
        public static IWebHostBuilder UseCloudBillingSerilog(this IWebHostBuilder builder, Action<WebHostBuilderContext, LoggerConfiguration> configureLogger = null)
        {
            builder.UseSerilog((context, configuration) =>
             {
                 configuration
                     .Enrich.FromLogContext()
                     .Enrich.WithDemystifiedStackTraces()
                     .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                     .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                     .Enrich.WithProperty("Version", GetVersion())
                     .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                     .MinimumLevel.Override("System", LogEventLevel.Warning)
                     .ReadFrom.Configuration(context.Configuration);

                 configureLogger?.Invoke(context, configuration);
             });

            return builder;
        }

        private static string GetVersion()
        {
            return Assembly
                .GetEntryAssembly()
                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
                .InformationalVersion;
        }
    }
}
