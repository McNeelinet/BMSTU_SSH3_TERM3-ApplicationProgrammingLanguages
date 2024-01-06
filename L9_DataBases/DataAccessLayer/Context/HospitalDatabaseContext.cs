using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public sealed class HospitalDatabaseContext : DbContext
{
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Username=hospital_db_admin;Password=89089274092");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Specialization>()
            .HasKey(s => s.Id);
        modelBuilder.Entity<Specialization>()
            .Property(s => s.Name).IsRequired();

        modelBuilder.Entity<Doctor>()
            .HasKey(d => d.Id);
        modelBuilder.Entity<Doctor>()
            .Property(d => d.SpecializationId).IsRequired();
        modelBuilder.Entity<Doctor>()
            .Property(d => d.Name).IsRequired();

        modelBuilder.Entity<Certificate>()
            .HasKey(c => c.Id);
        modelBuilder.Entity<Certificate>()
            .Property(c => c.DoctorId).IsRequired();
        modelBuilder.Entity<Certificate>()
            .Property(c => c.Description).IsRequired();
        modelBuilder.Entity<Certificate>()
            .Property(c => c.Date).IsRequired();
    }
}