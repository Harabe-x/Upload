namespace ImageVault.ImageService.Data.Dtos.Image;

public record EditImageDto(string ApiKey, string ImageKey,string NewTitle, string NewDescription);