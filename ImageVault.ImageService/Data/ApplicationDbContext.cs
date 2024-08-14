using Microsoft.EntityFrameworkCore;

namespace ImageVault.ImageService.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
}