using ImageVault.AuthenticationService.Configuration;
using ImageVault.AuthenticationService.Data.Interfaces.Auth;
using ImageVault.AuthenticationService.Data.Interfaces.RabbitMq;

namespace ImageVault.AuthenticationService.Services;

/// <summary>
///     Provides JWT Tokens to other services
/// </summary>
public class ServicesTokenProvider : BackgroundService
{
    private const int TokenRefreshInterval = 30;

    private const int RetryInterval = 15;
    private readonly IConfiguration _configuration;

    private readonly ILogger<ServicesTokenProvider> _logger;

    private readonly IServiceScopeFactory _scopeFactory;

    public ServicesTokenProvider(IConfiguration configuration, IServiceScopeFactory scopeFactory,
        ILogger<ServicesTokenProvider> logger)
    {
        _configuration = configuration;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    /// <summary>
    ///  Sends JWT tokens to other services at 30-minute intervals
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <exception cref="NullReferenceException"></exception>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
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

            var token = tokenService.CreateToken(operationResult.Value, "Admin");
            _logger.LogInformation($"Token sent over amqp. \n Token:  {token} ");

            messageSender.SendMessage(token, _configuration.GetUploadServiceJwtQueue());

            await Task.Delay(TimeSpan.FromMinutes(TokenRefreshInterval), stoppingToken);
        }
    }
}