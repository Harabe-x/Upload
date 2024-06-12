using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.Server.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{

    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
    {

    }

    public DbSet<ImageCollection> ImageCollections { get; set;  }

    public DbSet<Image> Images { get; set;  }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var roles = new List<IdentityRole>
        {
            new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new()
            {
                Name = "User",
                NormalizedName = "USER"
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}