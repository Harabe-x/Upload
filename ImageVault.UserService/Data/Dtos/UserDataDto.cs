namespace ImageVault.UserService.Data.Dtos;

public record UserDataDto
(
    string FirstName, 
    string LastName, 
    string DataTheme, 
    string Email, 
    string profilePictureUrl
);