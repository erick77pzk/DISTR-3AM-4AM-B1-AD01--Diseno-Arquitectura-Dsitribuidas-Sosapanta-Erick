using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        private readonly CatalogoDbContext _context;

        public MarcasController(CatalogoDbContext context)
        {
            _context = context;
        }

        // GET: api/Marcas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarcas()
        {
            return await _context.Marcas.ToListAsync();
        }

        // GET: api/Marcas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarca(int id)
        {
            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.IdMarca == id);

            if (marca == null)
            {
                return NotFound();
            }

            return marca;
        }

        // POST: api/Marcas
        [HttpPost]
        public async Task<ActionResult<Marca>> PostMarca(
            Marca marca)
        {
            _context.Marcas.Add(marca);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetMarca),
                new { id = marca.IdMarca },
                marca
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutMarca(
            int id,
            Marca marca)
        {
            if (id != marca.IdMarca)
            {
                return BadRequest();
            }

            _context.Entry(marca).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            var marca = await _context.Marcas
                .FindAsync(id);

            if (marca == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marca);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}