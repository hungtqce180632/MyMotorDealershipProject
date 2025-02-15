namespace MyMotorDealership.Web.ViewModels.Posts
{
    using Motors;

    public class PostInListViewModel
    {
        public MotorInListViewModel Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}