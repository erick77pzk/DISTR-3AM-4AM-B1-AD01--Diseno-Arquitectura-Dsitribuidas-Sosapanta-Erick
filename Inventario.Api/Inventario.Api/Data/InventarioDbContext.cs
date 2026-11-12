
using Inventario.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Inventario.Api.Data
{
    public class InventarioDbContext : DbContext
    {
        public InventarioDbContext(
            DbContextOptions<InventarioDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Existencia> Existencias { get; set; }
        public DbSet<MovimientoInventario> MovimientosInventario { get; set; }
        public DbSet<ReservaStock> ReservasStock { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Existencia>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(e => e.IdBodega)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MovimientoInventario>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(m => m.IdBodega)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservaStock>()
                .HasOne<Bodega>()
                .WithMany()
                .HasForeignKey(r => r.IdBodega)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
