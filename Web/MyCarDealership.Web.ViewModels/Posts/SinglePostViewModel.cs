namespace MyMotorDealership.Web.ViewModels.Posts
{
    using Motors;

    public class SinglePostViewModel
    {
        public SingleMotorViewModel Motor { get; init; }

        public string PublishedOn { get; init; }

        public string SellerName { get; set; }

        public string SellerPhoneNumber { get; set; }
    }
}