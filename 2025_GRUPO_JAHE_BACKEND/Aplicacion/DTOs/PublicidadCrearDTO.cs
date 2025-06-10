using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class PublicidadCrearDTO
    {
        public string EnlacePublicidad { get; set; } = string.Empty;
        public byte[]? Imagen { get; set; }
        public string NombreArchivo { get; set; } = string.Empty;
    }
}