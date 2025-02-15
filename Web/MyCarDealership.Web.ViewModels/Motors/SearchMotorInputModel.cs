namespace MyMotorDealership.Web.ViewModels.Motors
{
    using System.ComponentModel.DataAnnotations;
    using CustomAttributes.ValidationAttributes;

    using static Data.Common.DataConstants;

    public class SearchMotorInputModel : BaseMotorInputModel
    {
        [Display(Name = "Make, model or/and specification:")]
        public string TextSearchTerm { get; set; }

        [RangeUntilCurrentYear(
            MotorYearMinValue,
            ErrorMessage = "The Motor year must be between {1} and {2}.")]
        public int? FromYear { get; set; }

        [RangeUntilCurrentYear(
            MotorYearMinValue,
            ErrorMessage = "The Motor year must be between {1} and {2}.")]
        public int? ToYear { get; set; }

        [Range(
            MotorHorsepowerMinValue,
            MotorHorsepowerMaxValue,
            ErrorMessage = "The Motor horsepower must be between {1} and {2}.")]
        public int? MinHorsepower { get; set; }

        [Range(
            MotorHorsepowerMinValue,
            MotorHorsepowerMaxValue,
            ErrorMessage = "The Motor horsepower must be between {1} and {2}.")]
        public int? MaxHorsepower { get; set; }

        [RangeWithCustomFormat(MotorPriceMinValue, MotorPriceMaxValue, "Motor price")]
        [Display(Name = "Minimum price (in Euro):")]
        public decimal? MinPrice { get; set; }

        [RangeWithCustomFormat(MotorPriceMinValue, MotorPriceMaxValue, "Motor price")]
        [Display(Name = "Maximum price (in Euro):")]
        public decimal? MaxPrice { get; set; }
    }
}