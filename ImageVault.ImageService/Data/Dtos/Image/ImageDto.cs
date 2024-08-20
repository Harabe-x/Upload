namespace ImageVault.ImageService.Data.Dtos.Image;

public record ImageDto(string Key, string ImageUrl, string CollectionName ,string Title , string Description,string ImageFormat, DateTime CreatedAt );