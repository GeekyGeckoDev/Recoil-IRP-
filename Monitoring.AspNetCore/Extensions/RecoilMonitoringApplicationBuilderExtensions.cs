
using Microsoft.AspNetCore.Builder;
using Monitoring.AspNetCore.Middleware;

namespace Monitoring.AspNetCore.Extensions
{
    public static class RecoilMonitoringApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRecoilMonitoring(
            this IApplicationBuilder app)
        {
            return app.UseMiddleware<HttpMonitoringMiddleware>();
        }
    }
}
