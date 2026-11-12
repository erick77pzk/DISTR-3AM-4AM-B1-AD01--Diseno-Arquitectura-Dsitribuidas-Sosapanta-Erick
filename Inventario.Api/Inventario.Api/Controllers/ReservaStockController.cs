
using Inventario.Api.Data;
using Inventario.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaStockController : ControllerBase
    {
        private readonly InventarioDbContext _context;

        public ReservaStockController(InventarioDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservaStock>>>
            GetReservas()
        {
            return await _context.ReservasStock.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservaStock>>
            GetReserva(int id)
        {
            var reserva =
                await _context.ReservasStock.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        
        [HttpPost]
        public async Task<ActionResult<ReservaStock>>
            CrearReserva(ReservaStock reserva)
        {
            _context.ReservasStock.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetReserva),
                new { id = reserva.IdReserva },
                reserva
            );
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarReserva(
            int id,
            ReservaStock reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarReserva(int id)
        {
            var reserva =
                await _context.ReservasStock.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            _context.ReservasStock.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
