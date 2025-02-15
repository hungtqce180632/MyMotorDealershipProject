namespace MyMotorDealership.Services.Posts.Models
{
    using System.Collections.Generic;
    using Motors.Models;

    public class PostFormInputModelDTO
    {
        public MotorFormInputModelDTO Motor { get; set; }

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();
        
        public string SellerName { get; set; }
        
        public string SellerPhoneNumber { get; set; }
    }
}
