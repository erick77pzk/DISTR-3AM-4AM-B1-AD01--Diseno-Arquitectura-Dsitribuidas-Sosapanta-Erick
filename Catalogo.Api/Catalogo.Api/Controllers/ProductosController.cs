using Catalogo.Api.Data;
using Catalogo.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalogo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly CatalogoDbContext _context;

        public ProductosController(CatalogoDbContext context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        // GET: api/Productos/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<ActionResult<Producto>> PostProducto(
            Producto producto)
        {
            // Verificar que exista la categoría
            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.IdCategoria == producto.IdCategoria);

            if (!categoriaExiste)
            {
                return BadRequest("La categoría no existe.");
            }

            // Verificar que exista la marca
            var marcaExiste = await _context.Marcas
                .AnyAsync(m => m.IdMarca == producto.IdMarca);

            if (!marcaExiste)
            {
                return BadRequest("La marca no existe.");
            }

            _context.Productos.Add(producto);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetProducto),
                new { id = producto.IdProducto },
                producto
            );
        }

        // PUT: api/Productos/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(
            int id,
            Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return BadRequest();
            }

            // Verificar categoría
            var categoriaExiste = await _context.Categorias
                .AnyAsync(c => c.IdCategoria == producto.IdCategoria);

            if (!categoriaExiste)
            {
                return BadRequest("La categoría no existe.");
            }

            // Verificar marca
            var marcaExiste = await _context.Marcas
                .AnyAsync(m => m.IdMarca == producto.IdMarca);

            if (!marcaExiste)
            {
                return BadRequest("La marca no existe.");
            }

            _context.Entry(producto).State =
                EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Productos/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos
                .FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}