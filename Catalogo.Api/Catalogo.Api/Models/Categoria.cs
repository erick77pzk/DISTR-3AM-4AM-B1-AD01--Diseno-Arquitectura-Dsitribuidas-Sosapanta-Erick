using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdCategoria")]
        public int IdCategoria { get; set; }

        [Required]
        [Column("Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("Descripcion")]
        [StringLength(100)]
        public string Descripcion { get; set; } = string.Empty;
    }
}