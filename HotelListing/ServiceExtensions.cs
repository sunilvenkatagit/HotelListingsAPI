using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<ApiUser>(options =>
            {
                options.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), services);
            builder.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();

            // Ref - KudVenkat
            //services.AddIdentity<ApiUser, IdentityRole>(options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //})
            //.AddEntityFrameworkStores<DatabaseContext>().AddDefaultTokenProviders();
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var key = Environment.GetEnvironmentVariable("KEY");
            // Key value: b08efa24-b9d2-4041-a31a-2e9d919e8689

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.GetSection("Issuer").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });
        }
        public static void ConfigureExceptionHanlder(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var errorContextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (errorContextFeature != null)
                    {
                        Log.Error($"Something went wrong in the {errorContextFeature.Error}");

                        await context.Response.WriteAsync(new Error
                        {
                            StatusCode = StatusCodes.Status500InternalServerError,
                            Message = "Internal Server Error. Please try again later!"
                        }
                        .ToString());
                    }
                });
            });
        }
    }
}
