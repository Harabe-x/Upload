using ImageVault.ClassLibrary.Validation.Interfaces;
using ImageVault.Server.Data.Dtos;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Data.Mappers;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ImageVault.Server.Repository;

public class UserAuthenticationRepository : IUserAuthenticationRepository   
{

    public UserAuthenticationRepository(UserManager<ApplicationUser> userManager,ILogger<UserAuthenticationRepository> logger,SignInManager<ApplicationUser> signInManager,IDataValidator dataValidator)
    {
        _logger = logger; 
        _userManager = userManager;
        _signInManager = signInManager;
        _dataValidator = dataValidator; 
    }

    public async Task<UserDatabaseOperationResultDto> CreateAccount(RegisterAccountDto accountDto)
    {

        if (!ValidateRegisterDto(accountDto)) return new UserDatabaseOperationResultDto(null,false);
        
        var user = accountDto.MapUser();
        
        var identityResult = await _userManager.CreateAsync(user, accountDto.Password);
        
        if (!identityResult.Succeeded)
        {
            _logger.LogWarning($"User registration data validation failed \nError:{string.Join("\n", identityResult.Errors.Select(x => x.Code))} ");
            
            return new UserDatabaseOperationResultDto(user, false);
        }
       
        var addingRoleResult = await  _userManager.AddToRoleAsync(user,"User");

        return new UserDatabaseOperationResultDto(user, addingRoleResult.Succeeded);
    }

    public async Task<UserDatabaseOperationResultDto> LoginUser(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null)
            return new UserDatabaseOperationResultDto(null, false);

        var isLoginSucceeded = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password, false);

        return new UserDatabaseOperationResultDto(user, isLoginSucceeded.Succeeded);
    }

    private bool ValidateRegisterDto(RegisterAccountDto accountDto)
    {
        return _dataValidator.ValidateData("ValidateName", accountDto.FirstName)
               && _dataValidator.ValidateData("ValidateName",accountDto.LastName)
               && _dataValidator.ValidateData("ValidatePassword",accountDto.Password); 
    }

    private readonly IDataValidator _dataValidator;
    
    private readonly ILogger<UserAuthenticationRepository> _logger;

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;
}