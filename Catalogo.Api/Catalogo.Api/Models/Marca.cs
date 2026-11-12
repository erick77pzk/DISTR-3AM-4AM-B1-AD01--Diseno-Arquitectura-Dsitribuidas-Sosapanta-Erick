using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalogo.Api.Models
{
    [Table("Marca")]
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdMarca")]
        public int IdMarca { get; set; }

        [Required]
        [Column("Nombre")]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [Column("PaisOrigen")]
        [StringLength(100)]
        public string PaisOrigen { get; set; } = string.Empty;
    }
}