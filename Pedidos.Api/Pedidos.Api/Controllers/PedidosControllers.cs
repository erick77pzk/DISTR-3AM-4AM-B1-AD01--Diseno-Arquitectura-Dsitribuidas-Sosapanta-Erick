using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Data;
using Pedidos.Api.Models;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosDbContext _context;

        public PedidosController(PedidosDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                .ToListAsync();
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.IdCliente == pedido.IdCliente);

            if (!clienteExiste)
            {
                return BadRequest("El cliente no existe.");
            }

            pedido.Fecha = DateTime.Now;

            pedido.Total = pedido.Detalles.Sum(
                d => d.Cantidad * d.PrecioUnitario);

            _context.Pedidos.Add(pedido);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetPedido),
                new { id = pedido.IdPedido },
                pedido);
        }

        // PUT: api/Pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            var clienteExiste = await _context.Clientes
                .AnyAsync(c => c.IdCliente == pedido.IdCliente);

            if (!clienteExiste)
            {
                return BadRequest("El cliente no existe.");
            }

            pedido.Total = pedido.Detalles.Sum(
                d => d.Cantidad * d.PrecioUnitario);

            _context.Entry(pedido).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.IdPedido == id);

            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}