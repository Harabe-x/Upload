using ImageVault.UserService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Data;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IConfiguration configuration ) : base(options)
    {
        _configuration = configuration;
    }
    
    public DbSet<UserModel> ApplicationUsers { get; set; }
}
