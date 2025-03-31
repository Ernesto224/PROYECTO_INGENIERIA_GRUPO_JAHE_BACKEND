using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("SobreNosotros")]
    public class SobreNosotros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSobreNosotros { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Imagen_SobreNosotros> ImagenesSobreNosotros { get; set; }
    }
}
