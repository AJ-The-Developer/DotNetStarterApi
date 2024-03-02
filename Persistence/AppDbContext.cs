using Application;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> opt)
    : IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>(opt), IAppDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Ignore<IdentityUserClaim<Guid>>();
        modelBuilder.Ignore<IdentityRoleClaim<Guid>>();
        modelBuilder.Ignore<IdentityUserToken<Guid>>();
        modelBuilder.Ignore<IdentityUserLogin<Guid>>();

        modelBuilder.Entity<User>().ToTable("Users", "Administration");
        modelBuilder.Entity<Role>().ToTable("Roles", "Administration");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles", "Administration");
    }
    
    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }
}