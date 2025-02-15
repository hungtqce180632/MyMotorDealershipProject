namespace MyMotorDealership.Services.Motors.Models
{
    public class LatestPostsMotorDTO : BaseMotorDTO
    {
        public int Horsepower { get; set; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public string CoverImage { get; init; }
    }
}