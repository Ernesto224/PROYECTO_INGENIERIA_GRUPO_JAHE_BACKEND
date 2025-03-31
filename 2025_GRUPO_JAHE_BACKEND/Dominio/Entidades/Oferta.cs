using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Oferta")]
    public class Oferta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOferta { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        [Required]
        public DateTime FechaFinal { get; set; }

        [Required]
        public int Porcentaje { get; set; }

        [Required]
        public bool Activo { get; set; }

        // Clave foránea
        [ForeignKey("TipoDeHabitacion")]
        public int IdTipoDeHabitacion { get; set; }
        public TipoDeHabitacion? TipoDeHabitacion { get; set; }
    }
}
