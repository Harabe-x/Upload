namespace ImageVault.UploadService.Data.Interfaces;

/// <summary>
/// Represents a consumer that interacts with RabbitMQ, providing methods to start and stop the consumer, 
/// as well as properties to track its state and performance metrics.
/// </summary>
public interface IRabbitMqConsumer : IDisposable
{
    /// <summary>
    /// Gets the name of the RabbitMQ consumer.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the name of the consumer.
    /// </value>
    string Name { get; }
    
    /// <summary>
    /// Gets the date and time when the consumer was started.
    /// </summary>
    /// <value>
    /// A <see cref="DateTime"/> indicating the start time of the consumer.
    /// </value>
    DateTime StartedAt { get; }
    
    /// <summary>
    /// Gets the total duration of time the consumer has been working.
    /// </summary>
    /// <value>
    /// A <see cref="TimeSpan"/> representing the work time of the consumer.
    /// </value>
    TimeSpan WorkTime { get; }
    
    /// <summary>
    /// Gets a value indicating whether the consumer is currently running.
    /// </summary>
    /// <value>
    /// <c>true</c> if the consumer is running; otherwise, <c>false</c>.
    /// </value>
    bool IsRunning { get; }
    
    /// <summary>
    /// Starts the RabbitMQ consumer.
    /// </summary>
    /// <remarks>
    /// This method initiates the connection to the RabbitMQ broker and begins consuming messages.
    /// </remarks>
    void Start();

    /// <summary>
    /// Stops the RabbitMQ consumer.
    /// </summary>
    /// <remarks>
    /// This method gracefully shuts down the consumer, ensuring that all in-flight messages are processed before stopping.
    /// </remarks>
    void Stop();
}