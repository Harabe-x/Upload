namespace ImageVault.UserService.Data.Dtos;

public record UpdateUserDto
(
    string FirstName, 
    string LastName, 
    string DataTheme, 
    string Email, 
    string ProfilePictureUrl
    );