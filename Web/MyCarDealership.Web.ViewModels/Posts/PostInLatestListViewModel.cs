namespace MyMotorDealership.Web.ViewModels.Posts
{
    using Motors;

    public class PostInLatestListViewModel
    {
        public LatestPostsMotorViewModel Motor { get; init; }

        public string PublishedOn { get; init; }
    }
}