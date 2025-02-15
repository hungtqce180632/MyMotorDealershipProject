namespace MyMotorDealership.Services.Motors.Models
{
    public class BaseMotorDTO
    {
        public int Id { get; init; }

        public string Make { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public decimal Price { get; init; }
    }
}