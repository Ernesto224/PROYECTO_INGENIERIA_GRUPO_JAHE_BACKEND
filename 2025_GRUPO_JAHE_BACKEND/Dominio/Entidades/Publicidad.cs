using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Publicidad")]
    public class Publicidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPublicidad { get; set; }

        [Required]
        public string Enlace { get; set; } = string.Empty;

        public bool Activa { get; set; } = true;

        // Clave foránea
        [ForeignKey("Imagen")]
        public int IdImagen { get; set; }
        public Imagen? Imagen { get; set; }
    }
}
