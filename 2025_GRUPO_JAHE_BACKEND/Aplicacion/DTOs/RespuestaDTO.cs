using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class RespuestaDTO<T>
    {
        public bool EsCorrecto { get; set; }
        public string? Texto { get; set; }
        public T? Objeto { get; set; }
    }
}
