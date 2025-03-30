using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Publicidad")]
    public class Publicidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPublicidad { get; set; }

        [Required]
        public string EnlacePublicidad { get; set; } = string.Empty;

        [Required]
        public bool Activo { get; set; }

        // Clave foránea
        [ForeignKey("Imagen")]
        public int IdImagen { get; set; }
        public Imagen? Imagen { get; set; }
    }
}
