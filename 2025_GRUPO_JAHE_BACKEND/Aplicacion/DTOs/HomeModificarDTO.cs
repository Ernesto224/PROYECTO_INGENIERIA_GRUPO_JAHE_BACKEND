using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class HomeModificarDTO
    {
        public int IdHome { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public required byte[] Imagen { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
    }
}
