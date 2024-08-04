using ImageVault.UploadService.Data.Interfaces.Services;

namespace ImageVault.UploadService.Services;

public class JwtTokenProvider : IJwtTokenProvider
{
    private  readonly object _lock = new(); 

    private string _token;

    public string Token
    {
        get
        {
            lock (_lock)
            {
                return _token;
            }
        }
        set
        {
            lock (_lock)
            { 
                _token = value;
            }
        }
    }

}