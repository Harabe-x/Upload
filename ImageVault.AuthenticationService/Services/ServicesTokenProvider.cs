using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace ImageVault.AuthenticationService.Services;


/// <summary>
///  Provides JWT Tokens to other services
/// </summary>
public class ServicesTokenProvider : BackgroundService
{
    private readonly IConfiguration _configuration;

    private readonly IServiceScopeFactory _scopeFactory;

    private readonly ILogger<ServicesTokenProvider> _logger;

    private const int TokenRefreshInterval = 30;

    private const int RetryInterval = 15;
    
    public ServicesTokenProvider(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<ServicesTokenProvider> logger )
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger; 
    }
    
    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateAsyncScope();

            var messageSender = scope.ServiceProvider.GetService<IRabbitMqMessageSender>();
            var tokenService = scope.ServiceProvider.GetService<IJwtTokenService>();
            var adminRepository = scope.ServiceProvider.GetService<IAdminUserRepository>();

            if (messageSender == null || tokenService == null || adminRepository == null)
            {
                _logger.LogError("One or more dependencies are not resolved.");

                throw new NullReferenceException();
            }
            
            var operationResult = await adminRepository.GetAdminUser();

            if (!operationResult.IsSuccess)
            {
                _logger.LogError($"Retrieving admin user failed, Error Message : {operationResult.Error} ");
                await Task.Delay(TimeSpan.FromSeconds(RetryInterval), stoppingToken);
                continue;
            }

            var token = tokenService.CreateToken(operationResult.Value);
            
            messageSender.SendMessage(token, _configuration.GetUploadServiceJwtQueue());

            await Task.Delay(TimeSpan.FromMinutes(TokenRefreshInterval), stoppingToken);
        }
    }
}