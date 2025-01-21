using Xunit;
using Prueba1_U1;

namespace TestProject
{
    public class CitasControllerTest
    {
        [Fact]
        public void CrearCita_ValidaDatosCita()
        {
            var cita = new Cita
            {
                PacienteId = 1,
                DoctorId = 1
            };

            Assert.NotNull(cita);

            Assert.True(cita.PacienteId > 0, "El ID del paciente debe ser mayor a 0.");
            Assert.True(cita.DoctorId > 0, "El ID del doctor debe ser mayor a 0.");
        }
    }
}
