using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NpgsqlTypes;

namespace MedicalProject.Models
{
    public class Patient
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        public ICollection<HealthData> HealthData { get; set; }
    }
}

