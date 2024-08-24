namespace ImageVault.UserService.Data.Dtos.Request;

/// <summary>
/// A record representing data sent to Metrics Service via AMQP
/// </summary>
/// <param name="UserId">Id of the user making the request </param>
/// <param name="TimeStamp">The time at which the user sent the request</param>
/// <param name="Endpoint">Endpoint to which was requested</param>
/// <param name="Ip"> The IP address of the person from whom the request comes </param>
/// <param name="Method">The HTTP method in which the request was sent</param>
public record Request
(
    string UserId,
    DateTime TimeStamp,
    string Endpoint,
    string Ip,
    string Method
);