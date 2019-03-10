using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CloudBilling.Shared.Authentication
{
    public static class Extensions
    {
        public static IServiceCollection AddCloudBillingAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("authentication");
            var authOptions = section.Get<AuthenticationOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.Authority = authOptions.Authority;
                    options.Audience = authOptions.Audience;
                });

            return services;
        }
    }
}
