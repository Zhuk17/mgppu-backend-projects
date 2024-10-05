using MedicalProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedicalProject.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<HealthData> HealthData { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Recommendation> Recommendations { get; set; }
    }
}