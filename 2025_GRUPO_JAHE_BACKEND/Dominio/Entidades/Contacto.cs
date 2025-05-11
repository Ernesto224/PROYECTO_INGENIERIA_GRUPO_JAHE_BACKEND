using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Contacto")]
    public class Contacto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdContacto { get; set; }

        [Required]
        public string Telefono1 { get; set; } = string.Empty;

        [Required]
        public string Telefono2 { get; set; } = string.Empty;

        [Required]
        public string ApartadoPostal { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}
