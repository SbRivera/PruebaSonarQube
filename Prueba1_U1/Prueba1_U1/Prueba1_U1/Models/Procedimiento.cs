using System.Text.Json.Serialization;

namespace Prueba1_U1.Models
{
    public class Procedimiento
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Costo { get; set; }

        // Solo necesitamos IdCita, no la referencia completa de Cita
        public int IdCita { get; set; }

        // Ignorar la propiedad Cita en la serialización (evita que se incluya en el cuerpo de la solicitud y respuesta)
        [JsonIgnore]
        public Cita Cita { get; set; }
    }
}
