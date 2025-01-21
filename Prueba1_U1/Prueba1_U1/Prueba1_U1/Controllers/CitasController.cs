using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba1_U1.Models;
using Prueba1_U1.Data;

namespace Prueba1_U1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CitasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            return await _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .ToListAsync();
        }

        // GET: api/citas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCitaById(int id)
        {
            var cita = await _context.Citas
                .Include(c => c.Doctor)
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cita == null)
            {
                return NotFound();
            }

            return cita;
        }

        // POST: api/citas
        [HttpPost]
        public async Task<ActionResult<Cita>> CreateCita([FromBody] Cita citaInput)
        {
            // Validar que los IDs de Doctor y Paciente sean válidos
            if (citaInput.DoctorId <= 0 || citaInput.PacienteId <= 0)
            {
                return BadRequest(new { message = "Debe proporcionar DoctorId y PacienteId válidos." });
            }

            // Crear la nueva cita solo con los IDs
            var cita = new Cita
            {
                DoctorId = citaInput.DoctorId,
                PacienteId = citaInput.PacienteId
            };

            try
            {
                // Agregar la cita al contexto y guardar los cambios
                _context.Citas.Add(cita);
                await _context.SaveChangesAsync();

                // Retornar la cita creada
                return CreatedAtAction(nameof(GetCitaById), new { id = cita.Id }, cita);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return StatusCode(500, new { message = $"Error al crear la cita: {ex.Message}" });
            }
        }

        // PUT: api/citas/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCita(int id, [FromBody] Cita citaInput)
        {
            if (id != citaInput.Id)
            {
                return BadRequest(new { message = "El ID de la cita no coincide con el proporcionado en la solicitud." });
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            // Actualizar los campos necesarios
            cita.DoctorId = citaInput.DoctorId;
            cita.PacienteId = citaInput.PacienteId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/citas/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}
