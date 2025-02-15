namespace MyMotorDealership.Services.Motors.Models
{
    public class SearchMotorInputModelDTO : BaseMotorInputModelDTO
    {
        public string TextSearchTerm { get; set; }
        
        public int? FromYear { get; set; }
        
        public int? ToYear { get; set; }
        
        public int? MinHorsepower { get; set; }
        
        public int? MaxHorsepower { get; set; }
        
        public decimal? MinPrice { get; set; }
        
        public decimal? MaxPrice { get; set; }
    }
}