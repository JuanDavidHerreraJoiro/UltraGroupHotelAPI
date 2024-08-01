using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraGroupHotelAPI.Application.Contracts.Identity;
using UltraGroupHotelAPI.Application.Models.Identity;
using UltraGroupHotelAPI.Identity.Models;
using UltraGroupHotelAPI.Identity.Services;

namespace UltraGroupHotelAPI.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection AddConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            string uriString = "https://www.google.com";

            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddDbContext<UltraGroupHotelIdentityDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"),
                b => b.MigrationsAssembly(typeof(UltraGroupHotelIdentityDbContext).Assembly.FullName)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UltraGroupHotelIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
                };
            });

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v3",
                    Title = "UltraGroupHotel",
                    Description = "Web Api para generar Tokens JWT",
                    TermsOfService = new Uri(uriString),
                    Contact = new OpenApiContact
                    {
                        Name = "Contact the developer",
                        Url = new Uri(uriString)
                    },
                    License = new OpenApiLicense
                    {
                        Name = "License",
                        Url = new Uri(uriString)
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                        Array.Empty<string>()
                        }
                    }
                );
            });



            return services;
        }
    }
}
