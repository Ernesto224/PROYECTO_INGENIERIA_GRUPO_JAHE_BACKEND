using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class HabitacionConsultaDTO
    {
        public int IdHabitacion { get; set; }
        public int Numero { get; set; }
        public TipoDeHabitacionConsultaDTO TipoDeHabitacion { get; set; }
    }
}
