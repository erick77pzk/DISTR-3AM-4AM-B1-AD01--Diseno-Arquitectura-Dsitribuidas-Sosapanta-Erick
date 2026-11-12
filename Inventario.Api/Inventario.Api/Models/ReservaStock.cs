
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventario.Api.Models
{
    [Table("ReservaStock")]
    public class ReservaStock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdReserva")]
        public int IdReserva { get; set; }

        [Required]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("IdBodega")]
        public int IdBodega { get; set; }

        [Required]
        [Column("Cantidad")]
        public int Cantidad { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; } = true;
    }
}
