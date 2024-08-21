namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;


/// <summary>
/// A record representing data sent to User Service via AMQP
/// </summary>
/// <param name="Id">User id</param>
/// <param name="FirstName">User first name</param>
/// <param name="LastName">User last name </param>
/// <param name="DataTheme">The user's preferred theme in the web application</param>
/// <param name="Email">User email address</param>
/// <param name="ProfilePictureUrl">Url to user profile picture</param>
public record UserData
(
    string Id,
    string FirstName,
    string LastName,
    string DataTheme,
    string Email,
    string ProfilePictureUrl
);