using System.ComponentModel.DataAnnotations;

namespace ImageVault.ApiKeyService.Data.Models;

/// <summary>
///  API key model that is stored in the database
/// </summary>
public class ApiKey
{
    public ApiKey()
    {
        Id = Guid.NewGuid().ToString();
        Key = Guid.NewGuid().ToString();
        StorageUsed = 0;
    }

    [Key] public string Id { get; set; }

    [Required] public string UserId { get; set; }

    [Required] public string KeyName { get; set; }

    [Required] public string Key { get; set; }
    
    [Required] public ulong StorageUsed { get; set; }
}