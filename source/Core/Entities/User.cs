using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MedicalProject.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; }
        public ICollection<HealthData> HealthData { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
    }
}