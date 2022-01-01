namespace CarShop.Service.Common.Providers.Cloudinary
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface ICloudinaryService
    {
        Task DeleteImageAsync(string fileName);
        Task<string[]> UploadPictureAsync(IFormFile pictureFile, string fileName, string username);
    }
}