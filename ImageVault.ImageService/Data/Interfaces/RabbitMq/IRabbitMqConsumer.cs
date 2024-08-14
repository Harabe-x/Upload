namespace ImageVault.ImageService.Data.Interfaces;

public interface IRabbitMqConsumer : IDisposable
{
    
    string Name { get; }
    
    DateTime StartedAt { get; }
    
    TimeSpan WorkTime { get; }
    
    bool IsRunning { get; }
    
    void Start();

    void Stop();
}