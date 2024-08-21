using System.ComponentModel.DataAnnotations;

namespace ImageVault.AuthenticationService.Data.Dtos.AuthDtos;


/// <summary>
///  A record representing the data necessary to log in
/// </summary>
/// <param name="Email">User email address</param>
/// <param name="Password">User password</param>
public record Login
(
    [Required] [EmailAddress] string Email,
    [Required] string Password
);