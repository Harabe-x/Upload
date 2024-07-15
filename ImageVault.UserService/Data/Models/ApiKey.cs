using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageVault.UserService.Data.Models;

public class ApiKey
{
    public string Id { get; set; } = new Guid().ToString(); 
    
    public string  UserId{ get; set; }

    public UserModel User { get; set; }
    
    public string Key { get; set; }
    
    public string KeyName { get; set; }
    
    [Column(TypeName = "decimal(6,2)") ]
    public decimal KeyCapacity { get; set; } 
    
    public DateTime CreatedAt { get; set; }
}

