namespace MyMotorDealership.Web.ViewModels.Posts
{
    using Motors;

    public class PostInAdminAreaViewModel
    {
        public BaseMotorViewModel Motor { get; init; }

        public string PublishedOn { get; init; }

        public bool IsPublic { get; init; }
    }
}
