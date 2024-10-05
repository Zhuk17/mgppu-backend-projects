using System;
using System.ComponentModel.DataAnnotations;

namespace MedicalProject.Core.Entities
{
    public class Recommendation
    {
        public int Id { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        public string RecommendationText { get; set; }

        public User User { get; set; }
    }
}