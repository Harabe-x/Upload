using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.Server.Data;

public class ApplicationDbContext : IdentityDbContext
{

    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
    {

    }

    public DbSet<ImageCollection> ImageCollections { get; set;  }
    
    public DbSet<Image> Images { get; set;  }
}