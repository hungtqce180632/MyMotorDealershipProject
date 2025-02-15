namespace MyMotorDealership.Services.Motors
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Data;
    using Data.Models;
    using Images;
    using Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class MotorsService : IMotorsService
    {
        private readonly MotorDealershipDbContext data;
        private readonly IImagesService imagesService;
        private readonly IConfigurationProvider mapperConfiguration;

        public MotorsService(MotorDealershipDbContext data, IImagesService imagesService, IMapper mapper)
        {
            this.data = data;
            this.imagesService = imagesService;
            this.mapperConfiguration = mapper.ConfigurationProvider;
        }
        
        public async Task<Motor> GetMotorFromInputModelAsync(MotorFormInputModelDTO inputMotor, List<int> selectedExtrasIds, string userId, string imagePath)
        {
            var Motor = new Motor()
            {
                Make = inputMotor.Make,
                Model = inputMotor.Model,
                Description = inputMotor.Description,
                CategoryId = inputMotor.CategoryId,
                FuelTypeId = inputMotor.FuelTypeId,
                TransmissionTypeId = inputMotor.TransmissionTypeId,
                Year = inputMotor.Year,
                Kilometers = inputMotor.Kilometers,
                Horsepower = inputMotor.Horsepower,
                Price = inputMotor.Price,
                LocationCountry = inputMotor.LocationCountry,
                LocationCity = inputMotor.LocationCity,
            };

            if (selectedExtrasIds.Any())
            {
                foreach (var extraId in selectedExtrasIds)
                {
                    var extra = this.data.Extras.FirstOrDefault(e => e.Id == extraId);

                    if (extra != null)
                    {
                        Motor.MotorExtras.Add(new MotorExtra
                        {
                            Extra = extra,
                            Motor = Motor,
                        });
                    }
                }
            }

            if (!inputMotor.Images.Any())
            {
                throw new Exception($"At least one Motor image is required.");
            }

            if (inputMotor.Images.Count() > 10)
            {
                throw new Exception($"The maximum allowed number of photos is 10.");
            }
            
            foreach (var image in inputMotor.Images)
            {
                var dbImage = await this.imagesService.UploadImageAsync(image, userId, imagePath);
                Motor.Images.Add(dbImage);
            }

            return Motor;
        }

        public async Task UpdateMotorDataFromInputModelAsync(int MotorId, MotorFormInputModelDTO inputMotor, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId)
        {
            var Motor = this.GetDbMotorById(MotorId);

            if (Motor == null)
            {
                throw new Exception($"Unfortunately, such Motor in our system doesn't exist!");
            }
            
            Motor.Make = inputMotor.Make;
            Motor.Model = inputMotor.Model;
            Motor.Description = inputMotor.Description;
            Motor.CategoryId = inputMotor.CategoryId;
            Motor.FuelTypeId = inputMotor.FuelTypeId;
            Motor.TransmissionTypeId = inputMotor.TransmissionTypeId;
            Motor.Year = inputMotor.Year;
            Motor.Kilometers = inputMotor.Kilometers;
            Motor.Horsepower = inputMotor.Horsepower;
            Motor.Price = inputMotor.Price;
            Motor.LocationCountry = inputMotor.LocationCountry;
            Motor.LocationCity = inputMotor.LocationCity;

            if (selectedExtrasIds.Any())
            {
                var currentExtrasIds = this.data.MotorExtras.Where(ce => ce.MotorId == MotorId).Select(ce => ce.ExtraId).ToList();

                foreach (var extraId in selectedExtrasIds)
                {
                    var extra = this.data.Extras.FirstOrDefault(e => e.Id == extraId);

                    if (extra != null && !currentExtrasIds.Contains(extraId))
                    {
                        Motor.MotorExtras.Add(new MotorExtra
                        {
                            Extra = extra,
                            Motor = Motor,
                        });
                    }
                }

                if (selectedExtrasIds.Count < currentExtrasIds.Count)
                {
                    var deletedExtrasIds = currentExtrasIds.Where(extraId => !selectedExtrasIds.Contains(extraId)).ToList();

                    foreach (var deletedExtraId in deletedExtrasIds)
                    {
                       var deletedMotorExtra = this.data.MotorExtras.First(ce => ce.MotorId == MotorId && ce.ExtraId == deletedExtraId);

                       this.data.MotorExtras.Remove(deletedMotorExtra);
                    }
                }
            }

            var currentImages = this.data.Images.Where(img => img.MotorId == MotorId).ToList();
            
            if (deletedImagesIds.Count >= currentImages.Count && !inputMotor.Images.Any())
            {
                throw new Exception($"You cannot delete all Motor images. At least one Motor image is required for each post.");
            }

            if (deletedImagesIds.Any())
            {
                foreach (var deletedImageId in deletedImagesIds)
                {
                    if (currentImages.Any(img => img.Id == deletedImageId))
                    {
                        var imageToRemove = this.data.Images.First(img => img.Id == deletedImageId);
                        this.data.Images.Remove(imageToRemove);
                    }
                }
            }

            var currentCoverImage = currentImages.FirstOrDefault(img => img.IsCoverImage);

            if (currentCoverImage == null || currentCoverImage.Id != coverImageId)
            {
                await this.imagesService.SetCoverImagePropertyAsync(coverImageId);
            }

            if (currentCoverImage != null && currentCoverImage.Id != coverImageId)
            {
                await this.imagesService.RemoveCoverImagePropertyAsync(currentCoverImage.Id);
            }

            if (inputMotor.Images != null)
            {
                if (inputMotor.Images.Count() + currentImages.Count > 10)
                {
                    throw new Exception($"The maximum allowed number of Motor images is 10.");
                }
                
                foreach (var image in inputMotor.Images)
                {
                    var dbImage = await this.imagesService.UploadImageAsync(image, userId, imagePath);
                    Motor.Images.Add(dbImage);
                }
            }

            await this.data.SaveChangesAsync();
        }

        public async Task DeleteMotorByIdAsync(int MotorId)
        {
            var Motor = this.GetDbMotorById(MotorId);

            Motor.IsDeleted = true;
            Motor.DeletedOn = DateTime.UtcNow;
            
            await this.data.SaveChangesAsync();
        }

        public IEnumerable<BaseMotorSpecificationServiceModel> GetAllCategories()
        {
            return this.data
                .Categories
                .ProjectTo<BaseMotorSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<BaseMotorSpecificationServiceModel> GetAllFuelTypes()
        {
            return this.data
                .FuelTypes
                .ProjectTo<BaseMotorSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<BaseMotorSpecificationServiceModel> GetAllTransmissionTypes()
        {
            return this.data
                .TransmissionTypes
                .ProjectTo<BaseMotorSpecificationServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public IEnumerable<MotorExtrasServiceModel> GetAllMotorExtras()
        {
            return this.data
                .Extras        
                .OrderBy(e => e.TypeId)
                .ProjectTo<MotorExtrasServiceModel>(this.mapperConfiguration)
                .ToList();
        }

        public void FillInputMotorBaseProperties(BaseMotorInputModelDTO inputMotor)
        {
            inputMotor.Categories = this.GetAllCategories();
            inputMotor.FuelTypes = this.GetAllFuelTypes();
            inputMotor.TransmissionTypes = this.GetAllTransmissionTypes();
            inputMotor.MotorExtras = this.GetAllMotorExtras();
        }

        private Motor GetDbMotorById(int MotorId)
        {
            return this.data.Motors.FirstOrDefault(c => c.Id == MotorId && !c.IsDeleted);
        }
    }
}