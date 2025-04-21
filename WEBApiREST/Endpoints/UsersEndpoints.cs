using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using WEBApiREST.Services;
using WEBApiREST.Users;
using System.Text.Json;

namespace WEBApiREST.Endpoints
{
    public static class UsersEndpoints
    {

        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app) 
        {
            var endpoints = app.MapGroup("user");
            endpoints.MapPost("register", Register);

            endpoints.MapPost("login", Login);

            endpoints.MapPost("test", Test).RequireAuthorization();



            return endpoints;
        }

        private static async Task<IResult> Test()
        {
            return Results.Ok();
        }

        private static async Task<IResult> Register(RegisterUserRequest request, UserService userService)
        {
            //
            await userService.Register(request.Username, request.Password);
            return Results.Ok();
        }
        private static async Task<IResult> Login(LoginUserRequest request , UserService userService, HttpContext context)
        {
            var token = await userService.Login(request.Username, request.Password);
            JSONToken JWTToken = new JSONToken { Token = token };
            context.Response.Cookies.Append("tasty-cookies", token);
            return Results.Ok(JsonSerializer.Serialize(JWTToken));
            //првоерить username и пароль
            //создать токен
            //сохранить токен в куки


        }


        
        
    }
}
