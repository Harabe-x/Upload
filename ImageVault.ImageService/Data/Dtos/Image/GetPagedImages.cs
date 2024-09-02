namespace ImageVault.ImageService.Data.Dtos.Image;

public record GetPagedImages(string ApiKey, string CollectionName, int Page = 1, int Limit = 10 );