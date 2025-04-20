using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ConsultaHabitacionesParametrosDTO
    {
        public int[] IdTiposHabitacion { get; set; }
        public DateTime FechaLlegada { get; set; }
        public DateTime FechaSalida { get; set; }
        public int NumeroDePagina { get; set; }
        public int MaximoDeDatos { get; set; }
        public bool IrALaUltimaPagina { get; set; }
    }
}
