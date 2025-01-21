using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba1_U1.Models;
using Prueba1_U1.Data;

namespace Prueba1_U1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcedimientosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProcedimientosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/procedimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procedimiento>>> GetProcedimientos()
        {
            return await _context.Procedimientos
                .Include(p => p.Cita) // Incluir la cita asociada
                .ToListAsync();
        }

        // GET: api/procedimientos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Procedimiento>> GetProcedimientoById(int id)
        {
            var procedimiento = await _context.Procedimientos
                .Include(p => p.Cita) // Incluir la cita asociada
                .FirstOrDefaultAsync(p => p.Id == id);

            if (procedimiento == null)
            {
                return NotFound();
            }

            return procedimiento;
        }

        // POST: api/procedimientos
        [HttpPost]
        public async Task<IActionResult> CreateProcedimiento([FromBody] Procedimiento procedimientoInput)
        {
            // Verifica que el IdCita esté presente y sea válido
            if (procedimientoInput.IdCita <= 0)
            {
                return BadRequest(new { message = "El idCita debe ser mayor que cero." });
            }

            // Buscar la Cita en la base de datos usando IdCita
            var cita = await _context.Citas.FindAsync(procedimientoInput.IdCita);

            // Verificar si la Cita existe
            if (cita == null)
            {
                return BadRequest(new { message = "No se encontró una cita con el id proporcionado." });
            }

            // Crear el nuevo Procedimiento
            var procedimiento = new Procedimiento
            {
                Descripcion = procedimientoInput.Descripcion,
                Costo = procedimientoInput.Costo,
                IdCita = procedimientoInput.IdCita
            };

            // Agregar el procedimiento al contexto
            _context.Procedimientos.Add(procedimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProcedimientoById), new { id = procedimiento.Id }, procedimiento);
        }

        // PUT: api/procedimientos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProcedimiento(int id, [FromBody] Procedimiento procedimientoInput)
        {
            if (id != procedimientoInput.Id)
            {
                return BadRequest(new { message = "El ID del procedimiento no coincide con el proporcionado en la solicitud." });
            }

            var procedimiento = await _context.Procedimientos.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            // Actualizar los campos necesarios
            procedimiento.Descripcion = procedimientoInput.Descripcion;
            procedimiento.Costo = procedimientoInput.Costo;
            procedimiento.IdCita = procedimientoInput.IdCita;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcedimientoExists(id))
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

        // DELETE: api/procedimientos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcedimiento(int id)
        {
            var procedimiento = await _context.Procedimientos.FindAsync(id);
            if (procedimiento == null)
            {
                return NotFound();
            }

            _context.Procedimientos.Remove(procedimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcedimientoExists(int id)
        {
            return _context.Procedimientos.Any(e => e.Id == id);
        }
    }
}
