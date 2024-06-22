using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Data;

public class MyContext : DbContext
{


    public MyContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<PrescriptionMedicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });
        

        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>
        {
            new Doctor { IdDoctor = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com" },
            new Doctor { IdDoctor = 2, FirstName = "Ann", LastName = "Smith", Email = "ann.smith@example.com" },
            new Doctor { IdDoctor = 3, FirstName = "Jack", LastName = "Taylor", Email = "jack.taylor@example.com" }
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>
        {
            new Patient { IdPatient = 1, FirstName = "Jane", LastName = "Doe", Birthdate = new DateTime(1980, 1, 1) },
            new Patient { IdPatient = 2, FirstName = "Mark", LastName = "Smith", Birthdate = new DateTime(1990, 2, 2) },
            new Patient { IdPatient = 3, FirstName = "Lucy", LastName = "Taylor", Birthdate = new DateTime(2000, 3, 3) }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>
        {
            new Medicament { IdMedicament = 1, Name = "Medicament1", Description = "Description1", Type = "Type1" },
            new Medicament { IdMedicament = 2, Name = "Medicament2", Description = "Description2", Type = "Type2" },
            new Medicament { IdMedicament = 3, Name = "Medicament3", Description = "Description3", Type = "Type3" }
        });
        
        
    }
    

}