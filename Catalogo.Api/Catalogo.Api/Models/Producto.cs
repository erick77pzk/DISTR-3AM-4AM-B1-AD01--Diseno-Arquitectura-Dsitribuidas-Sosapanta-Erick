using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [Required]
        [Column("IdMarca")]
        public int IdMarca { get; set; }

        [Required]
        [Column("Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("SKU")]
        [StringLength(100)]
        public string SKU { get; set; } = string.Empty;

        [Required]
        [Column("Precio", TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; } = true;
    }
}