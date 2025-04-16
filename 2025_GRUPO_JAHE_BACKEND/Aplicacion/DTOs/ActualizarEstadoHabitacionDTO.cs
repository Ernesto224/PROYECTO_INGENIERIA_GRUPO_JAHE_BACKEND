using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ActualizarEstadoHabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public string NuevoEstado { get; set; } = string.Empty;
    }
}
