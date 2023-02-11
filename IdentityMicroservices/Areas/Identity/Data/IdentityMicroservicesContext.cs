using IdentityMicroservices.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityMicroservices.Data;

public class IdentityMicroservicesContext : IdentityDbContext<ApplicationUser>
{
    public IdentityMicroservicesContext(DbContextOptions<IdentityMicroservicesContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationRole> ApplicationRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreateDate = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
