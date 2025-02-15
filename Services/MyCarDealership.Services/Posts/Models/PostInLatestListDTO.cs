namespace MyMotorDealership.Services.Posts.Models
{
    using Motors.Models;

    public class PostInLatestListDTO
    {
        public LatestPostsMotorDTO Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}