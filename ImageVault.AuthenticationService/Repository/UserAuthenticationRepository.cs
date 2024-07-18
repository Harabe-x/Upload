using System.Net;
using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data;
using ImageVault.AuthenticationService.Data.Dtos.AuthDtos;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Mappers;
using ImageVault.AuthenticationService.Data.Models;
using ImageVault.ClassLibrary.Validation.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace ImageVault.AuthenticationService.Repository;

public class UserAuthenticationRepository : IUserAuthenticationRepository
{
    private readonly IDataValidator _dataValidator;

    private readonly ILogger<UserAuthenticationRepository> _logger;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly ApplicationDbContext _dbContext;

    private readonly IConfiguration _configuration; 
    
    private readonly HttpClient _client;
    
    private readonly ITokenService _tokenService; 
    
    public UserAuthenticationRepository(UserManager<ApplicationUser> userManager,
        ILogger<UserAuthenticationRepository> logger, SignInManager<ApplicationUser> signInManager,
        IDataValidator dataValidator,ApplicationDbContext dbContext,ITokenService tokenService,IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _dataValidator = dataValidator;
        _dbContext = dbContext;
        _tokenService = tokenService;
        _configuration = configuration; 
        _client = new HttpClient();
    }

    public async Task<UserDatabaseOperationResultDto> CreateAccount(RegisterAccountDto accountDto)
    {
        if (!ValidateRegisterDto(accountDto))
            return new UserDatabaseOperationResultDto(null, false, new Error("Invalid user data"));

        var user = accountDto.MapUser();


        using var transaction = _dbContext.Database.BeginTransaction();
        
        var identityResult = await _userManager.CreateAsync(user, accountDto.Password);

        if (!identityResult.Succeeded)
        {
            _logger.LogWarning(
                $"User registration data validation failed \nError:{string.Join("\n", identityResult.Errors.Select(x => x.Code))} ");

            return new UserDatabaseOperationResultDto(user, false, new Error("invalid user data"));
        }
        

        var addingRoleResult = await _userManager.AddToRoleAsync(user, "User");

        var token = _tokenService.CreateToken(user);

        var requestMessage = BuildUserRegistrationRequest(token, user.FirstName, user.LastName, user.Email);

        try
        {
            using var response = await _client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode && addingRoleResult.Succeeded)
            {
                await transaction.CommitAsync();
            }
            else
            {
                _logger.LogError($"[{DateTime.Now}]  STATUS_CODE : {response.StatusCode}  REQUEST_MESSAGE : {response.RequestMessage} ");
                await transaction.RollbackAsync();
                return new UserDatabaseOperationResultDto(null,false, new Error("Cannot add new user right now"));
            }
        }
        catch (Exception e)
        {
            _logger.LogError( $"[{DateTime.Now}] MESSAGE: ${e.Message} SOURCE: ${e.Source}"  );
            await transaction.RollbackAsync();
            return new UserDatabaseOperationResultDto(null,false, new Error("Cannot add new user right now"));

        }
        
        return new UserDatabaseOperationResultDto(user, true, null);
    }

    public async Task<UserDatabaseOperationResultDto> LoginUser(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is not { })
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

    private HttpRequestMessage BuildUserRegistrationRequest(string token, string firstName, string lastName,string email)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            Headers =
            {
                { "Authorization" , $"Bearer {token}"}
            }, 
            RequestUri = new Uri(_configuration.GetEndpointUrl("UserServiceRegister")),
        };

        request.Content = JsonContent.Create(new
        {
            firstName,
            lastName,
            email,
            dataTheme = "dark",
            profilePictureUrl = "string"
        });

        return request;
    }
}