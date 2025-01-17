using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure.Persistance;
using Infrastructure.Repositories;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Services.Token;
using Infrastructure.Services.Token;
using Infrastructure.Services.Auth;
using Application.Services.Auth;

namespace Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        service.AddDbContext<AppDbContext>(
                       options => options.UseSqlite(connectionString)
                                         .EnableSensitiveDataLogging());

        service.AddIdentity<User, IdentityRole<Guid>>(options =>
                            options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddRoles<IdentityRole<Guid>>()
                            .AddDefaultTokenProviders();

        var jwtSettings = configuration.GetSection("JwtSettings");
        var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]!);

        service.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

        });


        service.AddScoped<ITokenService, TokenService>();
        service.AddScoped<IUserContext, UserContext>();
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<ICartRepository, CartRepository>();
    }

}