namespace MyMotorDealership.Web.ViewModels.Motors
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using CustomAttributes.ValidationAttributes;

    using static Data.Common.DataConstants;

    public class MotorFormInputModel : BaseMotorInputModel
    {
        [Required(ErrorMessage = "The Motor make field is required.")]
        [StringLength(
            MotorMakeMaxLength,
            MinimumLength = MotorMakeMinLength,
            ErrorMessage = "The Motor make must be between {2} and {1} characters long.")]
        [Display(Name = "Make:")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The Motor model field is required.")]
        [StringLength(
            MotorModelMaxLength,
            MinimumLength = MotorModelMinLength,
            ErrorMessage = "The Motor model must be between {2} and {1} characters long.")]
        [Display(Name = "Model:")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The Motor description field is required.")]
        [StringLength(
            int.MaxValue,
            MinimumLength = MotorDescriptionMinLength,
            ErrorMessage = "The Motor description must be at least {2} characters long.")]
        [Display(Name = "Description:")]
        public string Description { get; set; }

        [RangeUntilCurrentYear(
            MotorYearMinValue,
            ErrorMessage = "The Motor year must be between {1} and {2}.")]
        [Display(Name = "Year of first registration:")]
        public int Year { get; set; }

        [RangeWithCustomFormat(MotorPriceMinValue, MotorPriceMaxValue, "Motor price")]
        [Display(Name = "Price (in Euro):")]
        public decimal Price { get; set; }

        [RangeWithCustomFormat(MotorKilometersMinValue, MotorKilometersMaxValue, "Motor kilometers")]
        [Display(Name = "Mileage (in kilometers):")]
        public int Kilometers { get; set; }

        [Range(
            MotorHorsepowerMinValue,
            MotorHorsepowerMaxValue,
            ErrorMessage = "The Motor horsepower must be between {1} and {2}.")]
        [Display(Name = "Horsepower:")]
        public int Horsepower { get; set; }


        [Required(ErrorMessage = "The Motor country field is required.")]
        [StringLength(
            MotorLocationCountryMaxLength,
            MinimumLength = MotorLocationCountryMinLength,
            ErrorMessage = "The country name must be between {2} and {1} characters long.")]
        [Display(Name = "Motor location - Country:")]
        public string LocationCountry { get; set; }

        [Required(ErrorMessage = "The Motor city field is required.")]
        [StringLength(
            MotorLocationCityMaxLength,
            MinimumLength = MotorLocationCityMinLength,
            ErrorMessage = "The city name must be between {2} and {1} characters long.")]
        [Display(Name = "Motor location - City:")]
        public string LocationCity { get; set; }
        
        [Display(Name = "Images:")]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
