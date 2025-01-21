using Microsoft.EntityFrameworkCore;
using Prueba1_U1.Models;

namespace Prueba1_U1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Doctor> Doctores { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Procedimiento> Procedimientos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de las relaciones
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Paciente) // Relación Cita -> Paciente
                .WithMany()
                .HasForeignKey(c => c.PacienteId);

            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Doctor) // Relación Cita -> Doctor
                .WithMany()
                .HasForeignKey(c => c.DoctorId);

            // Configuración de la tabla Procedimientos
            modelBuilder.Entity<Procedimiento>()
                .Property(p => p.Costo)
                .HasColumnType("decimal(10,2)"); // Asegurar precisión del campo 'Costo'

            base.OnModelCreating(modelBuilder);
        }
    }
}
