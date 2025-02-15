namespace MyMotorDealership.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Motor
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(MotorMakeMaxLength)]
        public string Make { get; set; }

        [Required]
        [MaxLength(MotorModelMaxLength)]
        public string Model { get; set; }

        [Required]
        public string Description { get; set; }

        public int Year { get; set; }

        public decimal Price { get; set; }

        public int Kilometers { get; set; }

        public int Horsepower { get; set; }

        [Required]
        [MaxLength(MotorLocationCountryMaxLength)]
        public string LocationCountry { get; set; }

        [Required]
        [MaxLength(MotorLocationCityMaxLength)]
        public string LocationCity { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int FuelTypeId { get; set; }

        public FuelType FuelType { get; set; }

        public int TransmissionTypeId { get; set; }

        public TransmissionType TransmissionType { get; set; }
        
        public Post Post { get; set; }

        public ICollection<MotorExtra> MotorExtras { get; set; } = new HashSet<MotorExtra>();

        public ICollection<Image> Images { get; set; } = new HashSet<Image>();
    }
}