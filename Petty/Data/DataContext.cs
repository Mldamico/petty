using Microsoft.EntityFrameworkCore;
using Petty.Entities;

namespace Petty.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
}