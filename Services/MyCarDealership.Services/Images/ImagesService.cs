namespace MyMotorDealership.Services.Images
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Data;
    using Data.Models;

    public class ImagesService : IImagesService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly MotorDealershipDbContext data;

        public ImagesService(MotorDealershipDbContext data)
        {
            this.data = data;
        }

        public async Task<Image> UploadImageAsync(IFormFile image, string userId, string imagePath)
        {
            // /wwwroot/images/Motors/ce8f1a9e-6c4a-44a1-adf3-f7c3d54ff859.jpg
            Directory.CreateDirectory($"{imagePath}/Motors/");

            var extension = Path.GetExtension(image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(ex => extension.EndsWith(ex)))
            {
                throw new Exception($"Invalid image extension! The allowed extensions are {string.Join(", ", this.allowedExtensions)}.");
            }

            if (image.Length > (5 * 1024 * 1024))
            {
                throw new Exception($"Invalid file size. The maximum allowed file size is 5Mb.");
            }

            var dbImage = new Image
            {
                CreatorId = userId,
                Extension = extension,
                IsCoverImage = false,
            };
            
            var physicalPath = $"{imagePath}/Motors/{dbImage.Id}.{extension}";
            await using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            return dbImage;
        }
        
        public async Task SetCoverImagePropertyAsync(string imageId)
        {
            var image = this.data.Images.FirstOrDefault(img => img.Id == imageId);

            if (image != null)
            {
                image.IsCoverImage = true;
            }

            await this.data.SaveChangesAsync();
        }

        public async Task RemoveCoverImagePropertyAsync(string imageId)
        {
            var image = this.data.Images.FirstOrDefault(img => img.Id == imageId);

            if (image != null)
            {
                image.IsCoverImage = false;
            }

            await this.data.SaveChangesAsync();
        }

        public string GetDefaultMotorImagesPath(string imageId, string imageExtension)
        {
            return $"/images/Motors/{imageId}.{imageExtension}";
        }

        public string GetCoverImagePath(ICollection<Image> MotorImages)
        {
            var coverImage = MotorImages.FirstOrDefault(img => img.IsCoverImage);
            var imageId = coverImage != null ? coverImage.Id : MotorImages.First().Id;
            var imageExtension = coverImage != null ? coverImage.Extension : MotorImages.First().Extension;

            return this.GetDefaultMotorImagesPath(imageId, imageExtension);
        }
    }
}