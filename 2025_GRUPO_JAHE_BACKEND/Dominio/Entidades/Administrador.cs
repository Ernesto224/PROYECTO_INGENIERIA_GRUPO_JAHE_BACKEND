using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Administrador")]
    public class Administrador
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAdmin {  get; set; }

        [Required]
        public string NombreDeUsuario { get; set; } = string.Empty;

        [Required]
        public string Contrasennia {  get; set; } = string.Empty;
    }
}
