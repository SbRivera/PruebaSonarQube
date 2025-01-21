namespace Prueba1_U1.Models
{
    public class Cita
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }
        public Paciente? Paciente { get; set; } // Propiedad de navegación opcional

        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; } // Propiedad de navegación opcional
    }

}
