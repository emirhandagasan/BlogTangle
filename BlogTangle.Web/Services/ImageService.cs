using BlogTangle.Web.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogTangle.Web.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;

        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }

            return null;
        }
    }
}
