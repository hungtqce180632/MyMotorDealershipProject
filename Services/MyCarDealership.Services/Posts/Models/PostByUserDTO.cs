namespace MyMotorDealership.Services.Posts.Models
{
    using Motors.Models;

    public class PostByUserDTO
    {
        public MotorByUserDTO Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}