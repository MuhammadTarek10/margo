using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(
                       options => options.UseSqlite(connectionString)
                                         .EnableSensitiveDataLogging());

        services.AddIdentity<User, IdentityRole<Guid>>(options =>
                            options.SignIn.RequireConfirmedAccount = false)
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddRoles<IdentityRole<Guid>>()
                            .AddDefaultTokenProviders();

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
    }

}
