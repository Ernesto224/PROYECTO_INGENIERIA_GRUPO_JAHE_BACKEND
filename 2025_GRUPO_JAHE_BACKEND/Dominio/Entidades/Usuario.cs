using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario {  get; set; }

        [Required]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required]
        public string Contrasennia {  get; set; } = string.Empty;

        [Required]
        public string Rol { get; set; } = string.Empty;
    }
}
