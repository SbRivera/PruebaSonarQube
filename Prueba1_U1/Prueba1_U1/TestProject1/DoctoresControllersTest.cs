using Xunit;
using Prueba1_U1;

namespace TestProject
{
    public class DoctoresControllerTest
    {
        [Fact]
        public void CrearDoctor_ValidaDatosDoctor()
        {
            var doctor = new Doctor
            {
                Nombre = "Dra. Maria",
                Especialidad = "Cardiología",
                
            };

            Assert.NotNull(doctor);
            Assert.False(string.IsNullOrWhiteSpace(doctor.Nombre), "El nombre no puede estar vacío.");
            Assert.False(string.IsNullOrWhiteSpace(doctor.Especialidad), "La especialidad no puede estar vacía.");
            
        }
    }
}
