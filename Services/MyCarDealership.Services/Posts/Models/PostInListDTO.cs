namespace MyMotorDealership.Services.Posts.Models
{
    using Motors.Models;

    public class PostInListDTO
    {
        public MotorInListDTO Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}