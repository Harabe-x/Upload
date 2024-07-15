using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageVault.UserService.Data.Models;

public class ApiKey
{
    [Key]
    public string Id { get; set; } 

    [Required]
    public string UserId { get; set; }

    [ForeignKey("UserId")]
    public UserModel User { get; set; }

    [Required]
    public string Key { get; set; } 

    public string KeyName { get; set; }

    [Column(TypeName = "decimal(6,2)")]
    public decimal KeyStorageCapacity { get; set; }

    public DateTime CreatedAt { get; set; }

    public ApiKey()
    {
        Id = Guid.NewGuid().ToString();
        Key = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
    }
}

