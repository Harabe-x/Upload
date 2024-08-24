using ImageVault.UserService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.UserService.Data;

/// <summary>
/// /// Class responsible for managing the database context of the application,
/// </summary>
public class ApplicationDbContext : DbContext
{
    /// <summary>
    ///  Constructor for ApplicationDbContext
    /// </summary>
    /// <param name="options">DbContext configuration options</param>
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    /// <summary>
    ///  Representation of Users table
    /// </summary>
    public DbSet<UserModel> ApplicationUsers { get; set; }
}