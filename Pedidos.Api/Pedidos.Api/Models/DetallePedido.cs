using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Api.Models
{
    [Table("DetallePedido")]
    public class DetallePedido
    {
        [Key]
        [Column("IdDetalle")]
        public int IdDetalle { get; set; }

        [Required]
        [Column("IdPedido")]
        public int IdPedido { get; set; }

        [Required]
        [Column("IdProducto")]
        public int IdProducto { get; set; }

        [Required]
        [Column("Cantidad")]

        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey("IdPedido")]
        public Pedido? Pedido { get; set; }
    }
}