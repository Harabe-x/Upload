using System.ComponentModel.DataAnnotations;

namespace ImageVault.UploadService.Data.Dtos.Upload;

/// <summary>
///  Represents the data that will be used to send the image
/// </summary>
/// <param name="Image">Image to be uploaded <see cref="IFormFile"/></param>
/// <param name="CollectionName"></param>
/// <param name="Title">Image title</param>
/// <param name="Description">Image description</param>
/// <param name="UseCompression">A flag specifying whether to use compression or not</param>
public record ImageUploadData([Required]IFormFile Image,[Required]string ApiKey, string CollectionName, string Title, string Description, bool UseCompression);