namespace ImageVault.UserService.Data.Dtos;


/// <summary>
///  A record representing the data needed to update the user's data
/// </summary>
/// <param name="FirstName">User first name</param>
/// <param name="LastName">User last name</param>
/// <param name="DataTheme">The user's preferred theme in the web application</param>
/// <param name="Email">User email address</param>
/// <param name="ProfilePictureUrl">Url to user profile picture</param>
public record UpdateUser
(
    string FirstName,
    string LastName,
    string DataTheme,
    string Email,
    string ProfilePictureUrl
);