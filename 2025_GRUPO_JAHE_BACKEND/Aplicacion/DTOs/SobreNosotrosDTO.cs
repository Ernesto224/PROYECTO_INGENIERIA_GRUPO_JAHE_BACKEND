using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class SobreNosotrosDTO
    {
        public int idSobreNosotros { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public List<ImagenDTO> Imagenes { get; set; }
    }
}
