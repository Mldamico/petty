using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Petty.Entities;

namespace Petty.Data;

public class DataContext : IdentityDbContext<AppUser, AppRole, int,
    IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
    IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<AppUser>().HasMany(user => user.UserRoles).WithOne(u => u.User)
            .HasForeignKey(user => user.UserId).IsRequired();
        builder.Entity<AppRole>().HasMany(role => role.UserRoles).WithOne(appuser => appuser.Role)
            .HasForeignKey(role => role.RoleId).IsRequired();
    }

    public DbSet<AppUser> Users { get; set; }
    
}