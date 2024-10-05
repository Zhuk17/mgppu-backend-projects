using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalProject.Core.Entities
{
    public class HealthData
    {
        public int Id { get; set; }

        [Required]
        public int PatientIdIdId { get; set; }

        [Required]
        public string Data { get; set; }

        public User User { get; set; }
    }
}