using GGWalks.API.Models.Domain;
using System.Net;

namespace GGWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
    }
}
