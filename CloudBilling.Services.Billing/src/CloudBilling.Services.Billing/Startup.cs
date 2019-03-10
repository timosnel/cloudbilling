using CloudBilling.Shared.Authentication;
using CloudBilling.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace CloudBilling.Services.Billing
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            _environment = environment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSwaggerGen(ConfigureSwaggerGen)
                .AddCloudBillingAuthentication(_configuration);

            services
                .AddMvcCore(ConfigureMvc)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters(ConfigureJsonFormatters)
                .AddCors();

            services.AddHealthChecks();
        }

        public void Configure(IApplicationBuilder app)
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseCors(ConfigureCors)
                .UseAuthentication()
                .UseHealthChecks("/health")
                .UseSwagger()
                .UseSwaggerUI(ConfigureSwaggerUI)
                .UseSecurityHeaders()
                .UseMvc();
        }

        private void ConfigureMvc(MvcOptions options)
        {
            options.ReturnHttpNotAcceptable = true;
        }

        private void ConfigureJsonFormatters(JsonSerializerSettings settings)
        {
            settings.Converters.Add(new StringEnumConverter());
        }

        private void ConfigureCors(CorsPolicyBuilder builder)
        {
            var origin = _environment.IsDevelopment() ? "" : "";

            builder
                .SetIsOriginAllowedToAllowWildcardSubdomains()
                .WithOrigins("*") // TODO: Fix
                .WithMethods("GET")
                .AllowCredentials();
        }

        private void ConfigureSwaggerGen(SwaggerGenOptions options)
        {
            options.SwaggerDoc("v1", new Info { Title = _environment.ApplicationName, Version = "v1" });
        }

        private void ConfigureSwaggerUI(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", _environment.ApplicationName);
            options.RoutePrefix = string.Empty;
        }
    }
}
