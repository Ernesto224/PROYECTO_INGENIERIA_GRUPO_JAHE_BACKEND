using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class TokenDeRespuestaDTO
    {
        public string TokenDeAcceso { get; set; } = string.Empty;
        public string TokenDeRefresco { get; set; } = string.Empty;
        public DateTime ExpiraEn { get; set; }
    }
}
