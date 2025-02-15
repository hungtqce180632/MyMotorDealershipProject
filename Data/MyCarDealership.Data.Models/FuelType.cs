namespace MyMotorDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class FuelType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(FuelTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Motor> Motors { get; set; } = new HashSet<Motor>();
    }
}