using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pedidos.Api.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdCliente")]
        public int IdCliente { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Nombres")]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        [Column("Cedula")]
        public string Cedula { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("Correo")]
        public string Correo { get; set; } = string.Empty;

        public ICollection<Pedido> Pedidos { get; set; }
            = new List<Pedido>();
    }
}