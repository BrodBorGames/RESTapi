using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace WEBApiREST.Extensions
{
    public static class AuthenticationExtensions
    {
        #region Public
        /// <summary>
        /// Добавляет настройки basic-аутентификации.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection" />.</param>
        /// <exception cref="ArgumentNullException">Значение <paramref name="services" /> не определено.</exception>
       

        /// <summary>
        /// Добавляет политику доступа.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection" />.</param>
        /// <returns></returns>
        /// <summary>
        /// Добавляет политику доступа для сотрудников киберстали.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection"/>.</param>
        /// <returns></returns>

        /// <summary>
        /// Добавляет политику доступа для редактирование.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection"/>.</param>
        /// <returns></returns>




        /// <summary>
        /// Добавляет настройки аутентификации.
        /// </summary>
        /// <param name="services">Экземпляр <see cref="IServiceCollection" />.</param>
        /// <param name="configuration">Экземпляр <see cref="IConfiguration" />.</param>
        /// <exception cref="ArgumentNullException">Значение <paramref name="services" /> не определено.</exception>
        public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);

            var authority = configuration.GetSection("IdentityClientConnection").GetSection("ServerAddress").Value;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                                  options =>
                                  {
                                      options.TokenValidationParameters.ValidateLifetime = false;
                                      options.TokenValidationParameters.ValidateAudience = false;
                                      options.TokenValidationParameters.ValidateTokenReplay = false;
                                      options.TokenValidationParameters.ValidateSignatureLast = false;
                                      options.TokenValidationParameters.ValidateIssuerSigningKey = false;
                                      options.TokenValidationParameters.ValidateActor = false;
                                      options.TokenValidationParameters.ValidateIssuer = false;
                                      

                                      options.Authority = authority;
                                      options.TokenValidationParameters.ClockSkew = TimeSpan.FromSeconds(5);
                                      options.RequireHttpsMetadata = false;
                                  });



            return services;
        }
        #endregion
    }
}
