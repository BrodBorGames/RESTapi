using WEBApiREST.Middleware;

namespace WEBApiREST.Extensions
{
    public static class TestMiddlewareDI
    {
        public static void AddTestMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<TestMiddleware>();
        }
    }
}
