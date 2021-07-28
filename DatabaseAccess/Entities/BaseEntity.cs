using System;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.Entities
{
    public class BaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }

        [Required]
        public string CreatedById { get; set; } // (FK)

        public User CreatedBy { get; set; }

        [Required]
        public string UpdatedById { get; set; } // (FK)

        public User UpdatedBy { get; set; } // (FK)
    }
}