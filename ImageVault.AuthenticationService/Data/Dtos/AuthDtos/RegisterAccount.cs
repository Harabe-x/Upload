using System.ComponentModel.DataAnnotations;

namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;

/// <summary>
///  A record representing the data necessary to register
/// </summary>
/// <param name="FirstName">User first name</param>
/// <param name="LastName">User last name</param>
/// <param name="Email">User email address</param>
/// <param name="DataTheme">The user's preferred theme in the web application</param>
/// <param name="Password">User password</param>
/// <param name="ProfilePictureUrl">Url to user profile picture</param>
public record RegisterAccount
(
    [Required] string FirstName,
    [Required] string LastName,
    [Required] [EmailAddress] string Email,
    [Required] string DataTheme,
    [Required] string Password,
    string ProfilePictureUrl
);