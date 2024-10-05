using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalProject.Core.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }

        [Required]
        public int PatientIdId { get; set; }

        [Required]
        public string Data { get; set; }
        public User User { get; set; }
    }
}