using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class HabitacionDTO
    {
        public int IdHabitacion { get; set; }
        public int IdTipoDeHabitacion { get; set; }
        public string Estado { get; set; }
        public TipoDeHabitacionDTO TipoDeHabitacion { get; set; }
    }
}
