namespace MyMotorDealership.Services.Posts.Models
{
    using System.Collections.Generic;
    using Motors.Models;

    public class SearchPostDTO
    {
        public SearchMotorInputModelDTO Motor { get; set; }

        public IEnumerable<int> SelectedCategoriesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedFuelTypesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedTransmissionTypesIds { get; set; } = new HashSet<int>();

        public IEnumerable<int> SelectedExtrasIds { get; set; } = new HashSet<int>();
    }
}