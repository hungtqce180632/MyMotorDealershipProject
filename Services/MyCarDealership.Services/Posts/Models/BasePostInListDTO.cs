namespace MyMotorDealership.Services.Posts.Models
{
    using Motors.Models;

    public class BasePostInListDTO
    {
        public BaseMotorDTO Motor { get; init; }

        public string PublishedOn { get; init; }

        public bool IsPublic { get; init; }
    }
}