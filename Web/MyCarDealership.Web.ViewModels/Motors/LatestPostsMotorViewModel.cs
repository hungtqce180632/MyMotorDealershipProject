namespace MyMotorDealership.Web.ViewModels.Motors
{
    public class LatestPostsMotorViewModel : BaseMotorViewModel
    {
        public int Horsepower { get; set; }

        public string FuelType { get; init; }

        public string TransmissionType { get; init; }

        public string CoverImage { get; init; }
    }
}