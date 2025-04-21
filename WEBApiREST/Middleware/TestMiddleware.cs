
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace WEBApiREST.Middleware
{
    public class TestMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            
            object contextHttp = context.Response; 
            await next(context);
        }
    }
}
