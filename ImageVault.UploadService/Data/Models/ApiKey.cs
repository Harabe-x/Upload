using System.ComponentModel.DataAnnotations;

namespace ImageVault.UploadService.Data.Models;

public class ApiKey
{
    [Required] public string Id { get; set; }
    
    [Required] public string UserId { get; set; }
    
    [Required] public string Key { get; set; }

    public ApiKey()
    {
        Id = Guid.NewGuid().ToString(); 
    }
}