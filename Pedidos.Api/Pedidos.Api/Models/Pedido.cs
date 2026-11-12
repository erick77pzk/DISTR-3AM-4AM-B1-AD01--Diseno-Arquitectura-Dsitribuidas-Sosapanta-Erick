using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Api.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdPedido")]
        public int IdPedido { get; set; }

        [Required]
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Required]
        [Column("Fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column("Total", TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        [Required]
        [Column("Estado")]
        public bool Estado { get; set; } = true;

        [ForeignKey(nameof(IdCliente))]
        public Cliente? Cliente { get; set; }

        public ICollection<DetallePedido> Detalles { get; set; }
            = new List<DetallePedido>();
    }
}