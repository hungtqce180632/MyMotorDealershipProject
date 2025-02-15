namespace MyMotorDealership.Web.ViewModels.Motors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BaseMotorInputModel
    {
        [Display(Name = "Category:")]
        public int CategoryId { get; init; }

        public IEnumerable<BaseMotorSpecificationViewModel> Categories { get; set; }

        [Display(Name = "Fuel type:")]
        public int FuelTypeId { get; init; }

        public IEnumerable<BaseMotorSpecificationViewModel> FuelTypes { get; set; }

        [Display(Name = "Transmission type:")]
        public int TransmissionTypeId { get; init; }

        public IEnumerable<BaseMotorSpecificationViewModel> TransmissionTypes { get; set; }

        [Display(Name = "Extras:")]
        public int MotorExtraId { get; init; }

        public IEnumerable<MotorExtrasViewModel> MotorExtras { get; set; }
    }
}
