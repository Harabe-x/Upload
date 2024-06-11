using ImageVault.Server.Data;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ImageVault.Server.Repository;

public class ImageCollectionRepository : IImageCollectionRepository
{
    public ImageCollectionRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public ImageCollection GetImageCollection(string id)
    {
        return _applicationDbContext.ImageCollections.FirstOrDefault(x => x.Id == id);
    }
    
    private readonly ApplicationDbContext _applicationDbContext;
}


