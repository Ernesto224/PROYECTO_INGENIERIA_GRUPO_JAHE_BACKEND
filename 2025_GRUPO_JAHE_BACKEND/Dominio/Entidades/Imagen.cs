using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Imagen")]
    public class Imagen
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdImagen { get; set; }

        [Required]
        public string Ruta { get; set; } = string.Empty;

        public bool Activa { get; set; } = true;
    }
}
