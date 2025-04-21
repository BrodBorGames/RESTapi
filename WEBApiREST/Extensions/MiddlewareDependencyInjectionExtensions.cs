using WEBApiREST.Middleware;

namespace WEBApiREST.Extensions
{
    public static class MiddlewareDependencyInjectionExtensions
    {
        public static void AddApplicationServices(this IApplicationBuilder app)
        {
            app.UseMiddleware<TokenServiceMiddleware>();
            ArgumentNullException.ThrowIfNull(app);

            
        }
    }
}
