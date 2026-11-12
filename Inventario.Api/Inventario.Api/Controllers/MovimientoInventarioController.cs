
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoInventarioController : ControllerBase
    {
        private readonly InventarioDbContext _context;

        public MovimientoInventarioController(
            InventarioDbContext context)
        {
            _context = context;
        }

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoInventario>>>
            GetMovimientos()
        {
            return await _context.MovimientosInventario.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoInventario>>
            GetMovimiento(int id)
        {
            var movimiento =
                await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // POST: 
        [HttpPost]
        public async Task<ActionResult<MovimientoInventario>>
            CrearMovimiento(MovimientoInventario movimiento)
        {
            _context.MovimientosInventario.Add(movimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMovimiento),
                new { id = movimiento.IdMovimiento },
                movimiento
            );
        }

      
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarMovimiento(
            int id,
            MovimientoInventario movimiento)
        {
            if (id != movimiento.IdMovimiento)
            {
                return BadRequest();
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarMovimiento(int id)
        {
            var movimiento =
                await _context.MovimientosInventario.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
