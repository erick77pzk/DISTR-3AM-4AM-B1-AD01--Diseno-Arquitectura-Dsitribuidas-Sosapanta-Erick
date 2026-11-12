
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegaController : ControllerBase
    {
        private readonly InventarioDbContext _context;

        public BodegaController(InventarioDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bodega>>> GetBodegas()
        {
            return await _context.Bodegas.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Bodega>> GetBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
            {
                return NotFound();
            }

            return bodega;
        }

        
        [HttpPost]
        public async Task<ActionResult<Bodega>> CrearBodega(Bodega bodega)
        {
            _context.Bodegas.Add(bodega);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBodega),
                new { id = bodega.IdBodega },
                bodega
            );
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarBodega(
            int id,
            Bodega bodega)
        {
            if (id != bodega.IdBodega)
            {
                return BadRequest();
            }

            _context.Entry(bodega).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarBodega(int id)
        {
            var bodega = await _context.Bodegas.FindAsync(id);

            if (bodega == null)
            {
                return NotFound();
            }

            _context.Bodegas.Remove(bodega);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
