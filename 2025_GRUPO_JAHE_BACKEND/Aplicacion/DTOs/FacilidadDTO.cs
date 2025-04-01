using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class FacilidadDTO
    {
        public int IdFacilidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public ImagenDTO? Imagen { get; set; }
    }
}
