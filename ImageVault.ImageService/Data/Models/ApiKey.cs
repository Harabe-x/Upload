using System.ComponentModel.DataAnnotations;

namespace ImageVault.ImageService.Data.Models;

public class ApiKey
{
    [Required]  public string Id { get; set; }

    [Required]  public string Key { get; set; }

    [Required] public string UserId { get; set; }


    public ApiKey()
    {
        Id = Guid.NewGuid().ToString(); 
    }
}