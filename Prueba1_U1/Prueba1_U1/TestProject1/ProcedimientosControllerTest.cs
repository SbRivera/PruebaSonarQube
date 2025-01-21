using Xunit;
using Prueba1_U1;

namespace TestProject
{
    public class ProcedimientosControllerTest
    {
        [Fact]
        public void CrearProcedimiento_ValidaDatosProcedimiento()
        {
            var procedimiento = new Procedimiento
            {
                Descripcion = "Extracción dental",
                Costo = 200.50m,
                IdCita = 1
            };

            Assert.NotNull(procedimiento);
            Assert.False(string.IsNullOrWhiteSpace(procedimiento.Descripcion), "La descripción no puede estar vacía.");
            Assert.True(procedimiento.Costo > 0, "El costo debe ser un valor positivo.");
            Assert.True(procedimiento.IdCita > 0, "El procedimiento debe estar asociado a una cita válida.");
        }
    }
}
