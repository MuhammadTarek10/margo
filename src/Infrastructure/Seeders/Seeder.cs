using Infrastructure.Persistance;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders;

internal class Seeder(AppDbContext context) : ISeeder
{
    public async Task Seed()
    {
        if (context.Database.GetPendingMigrations().Any()) await context.Database.MigrateAsync();

        if (!context.Database.CanConnect()) return;

        if (!context.Roles.Any())
        {
            IEnumerable<IdentityRole<Guid>> roles = GetRoles();
            context.AddRange(roles);
            await context.SaveChangesAsync();
        }

    }

    private IEnumerable<IdentityRole<Guid>> GetRoles()
    {
        List<IdentityRole<Guid>> roles =
                [
                new (Roles.User)
                {
                    NormalizedName = Roles.User.ToUpper()
                },
                new (Roles.Admin)
                {
                    NormalizedName = Roles.Admin.ToUpper()
                },
            ];

        return roles;
    }
}