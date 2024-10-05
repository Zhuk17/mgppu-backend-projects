using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalProject.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        public User User { get; set; }
    }
}