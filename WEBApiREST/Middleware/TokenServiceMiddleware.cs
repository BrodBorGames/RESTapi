using Microsoft.AspNetCore.Identity;
using WEBApiREST.Interfaces;

namespace WEBApiREST.Middleware
{
    public class TokenServiceMiddleware: IMiddleware
    {
        private readonly IJwtProvider _jwtProvider;
        public TokenServiceMiddleware(IJwtProvider jwtProvider)
        {
            _jwtProvider = jwtProvider ?? throw new ArgumentNullException(nameof(jwtProvider));
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine(context.Request);
            var accessToken = context.Request.Headers["Authorization"].FirstOrDefault();

            if (!string.IsNullOrEmpty(accessToken))
            {
                if (accessToken.Contains("Bearer"))
                {
                    accessToken = accessToken.Split(" ").Last();
                    var user = _jwtProvider.GetUserFromToken(accessToken);
                    //var storage = context.RequestServices.GetRequiredService<IUserStorage>();
                    //storage.SetUser(user);
                }
            }

            await next(context);
        }
    }
}
