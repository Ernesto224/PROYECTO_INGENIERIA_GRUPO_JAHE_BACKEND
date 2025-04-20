using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class RespuestaAutenticacionDTO
    {
        public string? Icon {  get; set; }
        public string? Text { get; set; }
        public TokenDeRespuestaDTO? TokenDeRespuesta { get; set; }
    }
}
