namespace MyMotorDealership.Services.Motors
{
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;
    using Data.Models;

    public interface IMotorsService
    {
        Task<Motor> GetMotorFromInputModelAsync(MotorFormInputModelDTO inputMotor, List<int> selectedExtrasIds, string userId, string imagePath);

        IEnumerable<BaseMotorSpecificationServiceModel> GetAllCategories();

        IEnumerable<BaseMotorSpecificationServiceModel> GetAllFuelTypes();

        IEnumerable<BaseMotorSpecificationServiceModel> GetAllTransmissionTypes();

        IEnumerable<MotorExtrasServiceModel> GetAllMotorExtras();

        void FillInputMotorBaseProperties(BaseMotorInputModelDTO inputMotor);

        Task UpdateMotorDataFromInputModelAsync(int MotorId, MotorFormInputModelDTO inputMotor, List<int> selectedExtrasIds, List<string> deletedImagesIds, string userId, string imagePath, string coverImageId);

        Task DeleteMotorByIdAsync(int MotorId);
    }
}