namespace ImageVault.ImageService.Data.Dtos.Image;

public record ImageDataDto(string Key, string ApiKey, string Collection, string Title, string Description,string UserId,ulong ImageSize,string FileFormat);
