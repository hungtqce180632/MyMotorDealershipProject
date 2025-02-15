namespace MyMotorDealership.Data.Models
{
    public class MotorExtra
    {
        public int MotorId { get; set; }

        public Motor Motor { get; set; }

        public int ExtraId { get; set; }

        public Extra Extra { get; set; }
    }
}