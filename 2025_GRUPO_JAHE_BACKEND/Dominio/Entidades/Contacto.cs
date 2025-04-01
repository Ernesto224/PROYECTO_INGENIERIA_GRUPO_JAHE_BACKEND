using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Contacto")]
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdContacto")]
        public int IdContacto { get; set; }
        [Column("Telefono1", TypeName = "nvarchar(max)")]
        public string Telefono1 { get; set; }
        [Column("Telefono2", TypeName = "nvarchar(max)")]
        public string Telefono2 { get; set; }
        [Column("ApartadoPostal", TypeName = "nvarchar(max)")]
        public string ApartadoPostal { get; set; }
        [Column("Email", TypeName = "nvarchar(max)")]
        public string Email { get; set; }
    }
}
