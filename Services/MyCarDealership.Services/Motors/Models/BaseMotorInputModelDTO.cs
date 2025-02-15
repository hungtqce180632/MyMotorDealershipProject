namespace MyMotorDealership.Services.Motors.Models
{
    using System.Collections.Generic;

    public class BaseMotorInputModelDTO
    {
        public int CategoryId { get; init; }

        public IEnumerable<BaseMotorSpecificationServiceModel> Categories { get; set; }
        
        public int FuelTypeId { get; init; }

        public IEnumerable<BaseMotorSpecificationServiceModel> FuelTypes { get; set; }
        
        public int TransmissionTypeId { get; init; }

        public IEnumerable<BaseMotorSpecificationServiceModel> TransmissionTypes { get; set; }
        
        public int MotorExtraId { get; init; }

        public IEnumerable<MotorExtrasServiceModel> MotorExtras { get; set; }
    }
}
