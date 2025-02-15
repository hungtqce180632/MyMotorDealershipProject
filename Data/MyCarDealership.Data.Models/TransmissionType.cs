namespace MyMotorDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class TransmissionType
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TransmissionTypeNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Motor> Motors { get; set; } = new HashSet<Motor>();
    }
}