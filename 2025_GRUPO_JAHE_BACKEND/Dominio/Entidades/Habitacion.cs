using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Habitacion")]
    public class Habitacion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHabitacion { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Estado { get; set; } = string.Empty;

        [ForeignKey("TipoDeHabitacion")]
        public int IdTipoDeHabitacion { get; set; }

        public TipoDeHabitacion TipoDeHabitacion { get; set; } = null!;

    }
}