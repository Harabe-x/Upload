// using System.Net;

using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using ImageVault.AuthenticationService.Data.Interfaces.Services;
using ImageVault.AuthenticationService.Data.Mappers;
using ImageVault.AuthenticationService.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace ImageVault.AuthenticationService.Repository;


/// <summary>
/// <inheritdoc cref="IUserAuthenticationRepository"/>
/// </summary>
public class UserAuthenticationRepository : IUserAuthenticationRepository
{
    private readonly IConfiguration _configuration;

    private readonly IJwtTokenService _jwtTokenService;

    private readonly ILogger<UserAuthenticationRepository> _logger;

    private readonly IRabbitMqMessageSender _rabbitMqMessageSender;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly IDataValidationService _validationService;

    public UserAuthenticationRepository(UserManager<ApplicationUser> userManager,
        ILogger<UserAuthenticationRepository> logger, SignInManager<ApplicationUser> signInManager,
        IDataValidationService validationService, IRabbitMqMessageSender rabbitMqMessageSender,
        IConfiguration configuration, IJwtTokenService jwtTokenService)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _validationService = validationService;
        _rabbitMqMessageSender = rabbitMqMessageSender;
        _configuration = configuration;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<OperationResult<AuthenticationResult>> CreateAccount(RegisterAccount account)
    {
        if (!_validationService.ValidateData("ValidateRegisterData",account))
            return new OperationResult<AuthenticationResult>(null, false, new Error("Invalid user data"));

        var user = account.MapUser();

        var identityResult = await _userManager.CreateAsync(user, account.Password);

        if (!identityResult.Succeeded)
        {
            _logger.LogWarning(
                $"User registration data validation failed \nError:{string.Join("\n", identityResult.Errors.Select(x => x.Code))} ");

            return new OperationResult<AuthenticationResult>(null, false,
                new Error("The provided data was invalid"));
        }

        var addingRoleResult = await _userManager.AddToRoleAsync(user, "User");

        if (!addingRoleResult.Succeeded)
            return new OperationResult<AuthenticationResult>(null, false,
                new Error("Sorry, something went wrong ..."));

        _rabbitMqMessageSender.SendMessage(account.MapToUserData(user.Id), _configuration.GetUserQueueName());

        return new OperationResult<AuthenticationResult>(await CreateAuthenticationResultDto(user), true, null);
    }

    public async Task<OperationResult<AuthenticationResult>> LoginUser(Login login)
    {

        if (_validationService.ValidateData("ValidateLoginData", login))
            return new OperationResult<AuthenticationResult>(null, false, new Error("Login data validation failed"));
        
        var user = await _userManager.FindByEmailAsync(login.Email);

        if (user == null)
            return new OperationResult<AuthenticationResult>(null, false, new Error("User does not exists"));

        var isLoginSucceeded = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

        if (!isLoginSucceeded.Succeeded)
            return new OperationResult<AuthenticationResult>(null, false, new Error("Invalid login credentials"));

        return new OperationResult<AuthenticationResult>(await CreateAuthenticationResultDto(user),
            isLoginSucceeded.Succeeded, null);
    }
    
    private async Task<AuthenticationResult> CreateAuthenticationResultDto(ApplicationUser user)
    {
        var role = await _userManager.GetRolesAsync(user);

        return new AuthenticationResult(user.Email, _jwtTokenService.CreateToken(user, role.FirstOrDefault()));
    }
}