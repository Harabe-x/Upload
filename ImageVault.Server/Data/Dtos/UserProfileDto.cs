namespace ImageVault.Server.Data.Dtos;

public class UserProfileDto
{
    public string? PreferedColorSchema { get; set; } 

    public string? ProfilePicture { get; set; }

    public string FirstName { get; set; }
      
    public string LastName { get; set; }
    
    public string Email { get; set; }
        
}