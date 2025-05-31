using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dominio.Entidades
{
    [Table("Temporada")]
    public class Temporada
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTemporada { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        [Required]
        public int Porcentaje { get; set; }

    }
}
