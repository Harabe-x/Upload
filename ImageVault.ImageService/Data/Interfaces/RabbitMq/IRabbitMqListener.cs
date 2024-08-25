namespace ImageVault.ImageService.Data.Interfaces;


/// <summary>
/// Represents a listener for RabbitMQ that can be started and stopped, with an option for resource disposal.
/// </summary>
public interface IRabbitMqListener : IDisposable
{
    /// <summary>
    /// Starts the RabbitMQ listener.
    /// </summary>
    /// <remarks>
    /// This method initiates the listening process, connecting to the RabbitMQ broker and beginning to receive messages.
    /// </remarks>
    void Start();

    /// <summary>
    /// Stops the RabbitMQ listener.
    /// </summary>
    /// <remarks>
    /// This method stops the listening process, disconnecting from the RabbitMQ broker and halting Message reception.
    /// </remarks>
    void Stop();
}