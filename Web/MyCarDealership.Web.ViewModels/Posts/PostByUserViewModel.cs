namespace MyMotorDealership.Web.ViewModels.Posts
{
    using Motors;

    public class PostByUserViewModel
    {
        public MotorByUserViewModel Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}