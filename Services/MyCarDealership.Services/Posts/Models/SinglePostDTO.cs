namespace MyMotorDealership.Services.Posts.Models
{
    using Motors.Models;

    public class SinglePostDTO
    {
        public SingleMotorDTO Motor { get; init; }

        public string PublishedOn { get; init; }

        public string SellerName { get; set; }

        public string SellerPhoneNumber { get; set; }
    }
}