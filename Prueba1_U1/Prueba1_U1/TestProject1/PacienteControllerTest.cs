using Xunit;
using Prueba1_U1; 

namespace TestProject
{
    public class PacientesControllerTest
    {
        [Fact]
        public void CrearPaciente_ValidaDatosPaciente()
        {
            var paciente = new Paciente
            {
                Nombre = "Juan",
                Apellido = "Perez",
                Email = "paciente@email.com"
            };

            Assert.NotNull(paciente);
            Assert.False(string.IsNullOrWhiteSpace(paciente.Nombre), "El nombre no puede estar vacío.");
            Assert.False(string.IsNullOrWhiteSpace(paciente.Apellido), "El apellido no puede estar vacío.");
        }
    }
}
