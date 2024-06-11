using ImageVault.Server.Models;

namespace ImageVault.Server.Data.Interfaces;

public interface IImageCollectionRepository
{
    public ImageCollection GetImageCollection(string id);
}