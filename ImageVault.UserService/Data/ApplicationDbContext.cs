using ImageVault.UserService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<UserModel?> ApplicationUsers { get; set; }
    
    public DbSet<ApiKey?> ApiKeys { get; set; }
}