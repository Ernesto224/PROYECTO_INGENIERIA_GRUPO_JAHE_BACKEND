using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class SobreNosotrosModificarDTO
    {
        public int IdSobreNosotros { get; set; }
        public int IdImagen { get; set; }
        public byte[]? Imagen { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
    }

}
