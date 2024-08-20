using Microsoft.EntityFrameworkCore;
using USFWebAPI.Entities;

namespace USFWebAPI
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Professional> Professionals { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Professionals - Appointments
            modelBuilder.Entity<Professional>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Professional)
                .OnDelete(DeleteBehavior.Restrict);

            //Specialties - Professionals
            modelBuilder.Entity<Specialty>()
                .HasMany(s => s.Professionals)
                .WithOne(p => p.Specialty)
                .OnDelete(DeleteBehavior.Restrict);

            //Specialties - Appointments
            modelBuilder.Entity<Specialty>()
                .HasMany(s => s.Appointments)
                .WithOne(a => a.Specialty)
                .OnDelete(DeleteBehavior.Restrict);

            //Patients - Appointments
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

    
}
