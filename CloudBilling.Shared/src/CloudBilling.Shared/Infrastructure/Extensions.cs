using Microsoft.AspNetCore.Builder;

namespace CloudBilling.Shared.Infrastructure
{
    public static class Extensions
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
            return app;
        }
    }
}
