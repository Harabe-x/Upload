namespace ImageVault.UploadService.Configuration;

/// <summary>
/// Provides methods for retrieving environment variables related to application configuration.
/// </summary>
public static class EnvironmentVariables
{
    /// <summary>
    /// Retrieves the RabbitMQ username from environment variables.
    /// </summary>
    /// <returns>
    /// The value of the "RABBITMQ_USERNAME" environment variable.
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// Thrown when the "RABBITMQ_USERNAME" environment variable is not found.
    /// </exception>
    public static string GetRabbitMqUsername()
    {
        return Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? 
               throw new NullReferenceException("Environment variable not found. Desired variable: RABBITMQ_USERNAME");
    }
    /// <summary>
    /// Retrieves the RabbitMQ password from environment variables.
    /// </summary>
    /// <returns>
    /// The value of the "RABBITMQ_PASSWORD" environment variable.
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// Thrown when the "RABBITMQ_PASSWORD" environment variable is not found.
    /// </exception>
    public static string GetRabbitMqPassword()
    {
        return Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? 
               throw new NullReferenceException("Environment variable not found. Desired variable: RABBITMQ_PASSWORD");
    }


    /// <summary>
    /// Retrieves the database connection string from environment variables.
    /// </summary>
    /// <returns>
    /// The value of the "DB_CONNECTION_STRING" environment variable.
    /// </returns>
    /// <exception cref="NullReferenceException">
    /// Thrown when the "DB_CONNECTION_STRING" environment variable is not found.
    /// </exception>
    public static string GetDatabaseConnectionString()
    {
        return Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? 
               throw new NullReferenceException("Environment variable not found. Desired variable: DB_CONNECTION_STRING");
    }
    
}
