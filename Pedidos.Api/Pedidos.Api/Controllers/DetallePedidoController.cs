
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pedidos.Api.Data;
using Pedidos.Api.Models;

namespace Pedidos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetallePedidoController : ControllerBase
    {
        private readonly PedidosDbContext _context;

        public DetallePedidoController(PedidosDbContext context)
        {
            _context = context;
        }

        // GET: api/DetallePedido
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePedido>>>
            GetDetalles()
        {
            return await _context.DetallesPedido.ToListAsync();
        }

        // GET: api/DetallePedido/1
        [HttpGet("{id}")]
        public async Task<ActionResult<DetallePedido>>
            GetDetalle(int id)
        {
            var detalle =
                await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            return detalle;
        }

        // POST: api/DetallePedido
        [HttpPost]
        public async Task<ActionResult<DetallePedido>>
            CrearDetalle(DetallePedido detalle)
        {
            _context.DetallesPedido.Add(detalle);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDetalle),
                new { id = detalle.IdDetalle },
                detalle
            );
        }

        // PUT: api/DetallePedido/1
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDetalle(
            int id,
            DetallePedido detalle)
        {
            if (id != detalle.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detalle).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/DetallePedido/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDetalle(int id)
        {
            var detalle =
                await _context.DetallesPedido.FindAsync(id);

            if (detalle == null)
            {
                return NotFound();
            }

            _context.DetallesPedido.Remove(detalle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
