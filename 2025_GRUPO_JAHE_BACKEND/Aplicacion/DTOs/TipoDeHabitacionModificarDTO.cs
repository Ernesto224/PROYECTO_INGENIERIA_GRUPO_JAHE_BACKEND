using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class TipoDeHabitacionModificarDTO
    {

        public int IdTipoDeHabitacion { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal TarifaDiaria { get; set; }

        public byte[]? Imagen { get; set; }

        public string NombreArchivo { get; set; } = string.Empty;
    }
}
