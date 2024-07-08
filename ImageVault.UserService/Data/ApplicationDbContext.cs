using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    
}