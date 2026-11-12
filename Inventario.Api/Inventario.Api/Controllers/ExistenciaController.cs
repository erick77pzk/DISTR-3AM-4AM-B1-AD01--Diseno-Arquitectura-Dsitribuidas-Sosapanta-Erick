
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExistenciaController : ControllerBase
    {
        private readonly InventarioDbContext _context;

        public ExistenciaController(InventarioDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Existencia>>> GetExistencias()
        {
            return await _context.Existencias.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Existencia>> GetExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
            {
                return NotFound();
            }

            return existencia;
        }

        
        [HttpPost]
        public async Task<ActionResult<Existencia>> CrearExistencia(
            Existencia existencia)
        {
            _context.Existencias.Add(existencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetExistencia),
                new { id = existencia.IdExistencia },
                existencia
            );
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarExistencia(
            int id,
            Existencia existencia)
        {
            if (id != existencia.IdExistencia)
            {
                return BadRequest();
            }

            _context.Entry(existencia).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarExistencia(int id)
        {
            var existencia = await _context.Existencias.FindAsync(id);

            if (existencia == null)
            {
                return NotFound();
            }

            _context.Existencias.Remove(existencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
