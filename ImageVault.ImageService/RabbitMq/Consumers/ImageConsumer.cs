using ImageVault.ImageService.Data.Interfaces;

namespace ImageVault.ImageService.RabbitMq.Consumers;

public class ImageConsumer : IRabbitMqConsumer
{

    public string Name => "ImageConsumer";

    public TimeSpan WorkTime => DateTime.Now - StartedAt;
    
    public DateTime StartedAt { get; private set; }
    
    public bool IsRunning { get; private set;  }

    private readonly IRabbitMqConnection _connection; 

    public ImageConsumer(IRabbitMqConnection connection)
    {
        _connection = connection; 
    }
    
    
    public void Start()
    {
        IsRunning = true;
        StartedAt = DateTime.Now; 
    }

    public void Stop()
    {
        IsRunning = false;
        _connection.Connection.Close();
    }
    public void Dispose()
    {
        if (!IsRunning) return;
        
        IsRunning = false;
        _connection.Connection?.Dispose();

    }

}