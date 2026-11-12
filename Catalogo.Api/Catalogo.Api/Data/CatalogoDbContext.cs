using Microsoft.EntityFrameworkCore;
using Catalogo.Api.Models;

namespace Catalogo.Api.Data
{
    public class CatalogoDbContext : DbContext
    {
        public CatalogoDbContext(
            DbContextOptions<CatalogoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Marca> Marcas { get; set; }

        public DbSet<Producto> Productos { get; set; }
    }
}