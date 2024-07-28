using System.ComponentModel.DataAnnotations;

namespace ImageVault.ApiKeyService.Data.Models;

public class ApiKey
{
    [Key]
    public string Id { get; set;  }
    
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public string KeyName { get; set; }
    
    [Required]
    public string Key { get; set; }
    
    [Required]
    public long StorageCapacity { get; set; }
    
}