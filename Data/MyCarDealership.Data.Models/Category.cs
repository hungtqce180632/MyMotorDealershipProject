﻿namespace MyMotorDealership.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class Category
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(CategoryNameMaxLength)]
        public string Name { get; set; }

        public ICollection<Motor> Motors { get; set; } = new HashSet<Motor>();
    }
}