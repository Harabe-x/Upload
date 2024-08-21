using ImageVault.AuthenticationService.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.AuthenticationService.Data;


/// <summary>
/// Class responsible for managing the database context of the application,
/// including identity management and user authentication.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    
    /// <summary>
    ///  Constructor for ApplicationDbContext
    /// </summary>
    /// <param name="dbContextOptions">DbContext configuration options</param>
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    /// <summary>
    /// Methods which defines Identity roles
    /// </summary>
    /// <param name="builder"></param>
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
            }
        };

        builder.Entity<IdentityRole>().HasData(roles);
    }
}