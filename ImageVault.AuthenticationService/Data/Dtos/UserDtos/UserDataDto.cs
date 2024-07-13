namespace ImageVault.AuthenticationService.Data.Dtos.UserDtos;

public record UserDataDto
(
    string FirstName,
    string LastName,
    string DataTheme,
    string Email,
    string ProfilePicture
);