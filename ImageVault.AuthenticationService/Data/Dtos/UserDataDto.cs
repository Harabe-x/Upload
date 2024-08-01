namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

public record UserDataDto
(
    string Id,
    string FirstName,
    string LastName,
    string DataTheme,
    string Email,
    string ProfilePictureUrl
);