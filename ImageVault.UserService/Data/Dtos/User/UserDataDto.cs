namespace ImageVault.UserService.Data.Dtos;

public record UserDataDto
(
    string Id, 
    string FirstName, 
    string LastName, 
    string DataTheme, 
    string Email, 
    string ProfilePictureUrl
);