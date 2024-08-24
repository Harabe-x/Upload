namespace ImageVault.UserService.Data.Dtos;



/// <summary>
/// A record representing user data
/// </summary>
/// <param name="Id">Unique user identifier.</param>
/// <param name="FirstName">User first name</param>
/// <param name="LastName">User last name</param>
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