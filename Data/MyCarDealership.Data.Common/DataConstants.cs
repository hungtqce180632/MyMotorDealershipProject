namespace MyMotorDealership.Data.Common
{
    public class DataConstants
    {
        //Motor constants
        public const int MotorMakeMaxLength = 30;
        public const int MotorMakeMinLength = 2;
        public const int MotorModelMaxLength = 30;
        public const int MotorModelMinLength = 2;
        public const int MotorDescriptionMinLength = 20;
        public const int MotorYearMinValue = 1950;
        public const int MotorPriceMinValue = 1;
        public const int MotorPriceMaxValue = 10000000;
        public const int MotorKilometersMinValue = 0;
        public const int MotorKilometersMaxValue = 2000000;
        public const int MotorHorsepowerMinValue = 5;
        public const int MotorHorsepowerMaxValue = 5000;
        public const int MotorLocationCountryMaxLength = 20;
        public const int MotorLocationCountryMinLength = 3;
        public const int MotorLocationCityMaxLength = 30;
        public const int MotorLocationCityMinLength = 3;

        //Post constants
        public const int PostSellerNameMaxLength = 30;
        public const int PostSellerNameMinLength = 2;
        public const int PostSellerPhoneNumberMaxLength = 20;
        public const int PostSellerPhoneNumberMinLength = 6;

        //Other data entities constants
        public const int CategoryNameMaxLength = 30;
        public const int ExtraNameMaxLength = 100;
        public const int ExtraTypeNameMaxLength = 20;
        public const int FuelTypeNameMaxLength = 40;
        public const int TransmissionTypeNameMaxLength = 30;
    }
}