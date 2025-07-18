﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio.Entidades
{
    [Table("Facilidades")]
    public class Facilidad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdFacilidad { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        // Clave foránea
        [ForeignKey("Imagen")]
        public int IdImagen { get; set; }
        public Imagen? Imagen { get; set; }
    }
}
