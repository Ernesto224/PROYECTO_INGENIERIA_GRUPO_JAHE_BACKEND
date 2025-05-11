using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Imagen_SobreNosotros")]
    public class Imagen_SobreNosotros
    {
        [ForeignKey("SobreNosotros")]
        public int IdSobreNosotros { get; set; }
        public SobreNosotros? SobreNosotros { get; set; }

        [ForeignKey("Imagen")]
        public int IdImagen { get; set; }
        public Imagen? Imagen { get; set; }
    }
}
