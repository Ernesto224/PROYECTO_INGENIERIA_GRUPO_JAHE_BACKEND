using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class EstadoHabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } = string.Empty;

        public TipoDeHabitacionDTO TipoDeHabitacion { get; set; } = null!;
    }
}
