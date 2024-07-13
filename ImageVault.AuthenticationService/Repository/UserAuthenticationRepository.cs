using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Mappers;
using ImageVault.AuthenticationService.Data.Models;
using ImageVault.ClassLibrary.Validation.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ImageVault.AuthenticationService.Repository;

public class UserAuthenticationRepository : IUserAuthenticationRepository
{
    private readonly IDataValidator _dataValidator;

    private readonly ILogger<UserAuthenticationRepository> _logger;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    public UserAuthenticationRepository(UserManager<ApplicationUser> userManager,
        ILogger<UserAuthenticationRepository> logger, SignInManager<ApplicationUser> signInManager,
        IDataValidator dataValidator)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _dataValidator = dataValidator;
    }

    public async Task<UserDatabaseOperationResultDto> CreateAccount(RegisterAccountDto accountDto)
    {
        if (!ValidateRegisterDto(accountDto))
            return new UserDatabaseOperationResultDto(null, false, new Error("Invalid user data"));

        var user = accountDto.MapUser();

        var identityResult = await _userManager.CreateAsync(user, accountDto.Password);

        if (!identityResult.Succeeded)
        {
            _logger.LogWarning(
                $"User registration data validation failed \nError:{string.Join("\n", identityResult.Errors.Select(x => x.Code))} ");

            return new UserDatabaseOperationResultDto(user, false, new Error("invalid user data"));
        }

        var addingRoleResult = await _userManager.AddToRoleAsync(user, "User");

        return new UserDatabaseOperationResultDto(user, addingRoleResult.Succeeded, null);
    }

    public async Task<UserDatabaseOperationResultDto> LoginUser(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
            return new UserDatabaseOperationResultDto(null, false, new Error("User does not exists"));

        var isLoginSucceeded = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        return new UserDatabaseOperationResultDto(user, isLoginSucceeded.Succeeded, null);
    }

    private bool ValidateRegisterDto(RegisterAccountDto accountDto)
    {
        return _dataValidator.ValidateData("ValidateName", accountDto.FirstName)
               && _dataValidator.ValidateData("ValidateName", accountDto.LastName)
               && _dataValidator.ValidateData("ValidatePassword", accountDto.Password);
    }
}