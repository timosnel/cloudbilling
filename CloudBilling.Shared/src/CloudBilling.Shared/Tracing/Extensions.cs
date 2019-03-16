using Jaeger;
using Jaeger.Samplers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;

namespace CloudBilling.Shared.Tracing
{
    public static class Extensions
    {
        public static IServiceCollection AddCloudBillingJaeger(this IServiceCollection services)
        {
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var hostingEnvironment = serviceProvider.GetRequiredService<IHostingEnvironment>();
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

                return new Tracer.Builder(hostingEnvironment.ApplicationName)
                    .WithSampler(new ConstSampler(true))
                    .WithLoggerFactory(loggerFactory)
                    .Build();
            });
         
            return services;
        }
    }
}
