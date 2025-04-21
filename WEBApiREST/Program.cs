using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI1.Repositories;
using WEBApiREST;
using WEBApiREST.Endpoints;
using WEBApiREST.Extensions;
using WEBApiREST.Interfaces;
using WEBApiREST.Interfaces.Interfaces;
using WEBApiREST.Services;
using WEBApiREST.Middleware;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var services = builder.Services;
// Add services to the container.


builder.Services.AddDbContext<ApplicationContext>(
    options =>
    {
        options.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationContext)));
    });
//services.Configure<JwtOptions>(configuration.GetSection(nameof(JwtOptions)));


//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
//                {
//                    options.TokenValidationParameters = new()
//                    {
//                        ValidateIssuer = false,
//                        ValidateAudience = false,
//                        ValidateLifetime = true,
//                        ValidateIssuerSigningKey = true,
//                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretkeysecretkeysecretkeysecretkeysecretkeysecretkeysecretkey"))
//                    };
//                    options.Events = new JwtBearerEvents
//                    {
//                        OnMessageReceived = context =>
//                        {
//                            context.Token = context.Request.Cookies["tasty-cookies"];
//                            return Task.CompletedTask;
//                        }
//                    };
//                });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsersRepository, UsersRepository>();


builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddCors();
services.AddBearerAuthentication(configuration);
services.AddScoped<TokenServiceMiddleware>();
services.AddScoped<TestMiddleware>();
services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        In = ParameterLocation.Header,
        Description = "Basic Authorization header using the Bearer scheme."
    });
    c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme()
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        Description = "Input bearer token to access this API",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    },
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "bearerAuth"
                            }
                        },
                        new string[] { }
                    }
                });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseDeveloperExceptionPage();
}
//app.UseCookiePolicy(new CookiePolicyOptions
//{
//    MinimumSameSitePolicy = SameSiteMode.Strict,
//    HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always,
//    Secure = CookieSecurePolicy.Always
//});

app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true).WithOrigins("http://localhost:4200", "http://localhost:8080") // allow any origin
                                                        //.WithOrigins("https://localhost:44351")); // Allow only this origin can also have multiple origins separated with comma
                    .AllowCredentials()); // allow credentials

app.AddTestMiddleware();

app.UseAuthentication();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapUsersEndpoints();
app.AddApplicationServices();


app.Run();
