using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ConsultaReservacionesParametrosDTO
    {
        public int NumeroDePagina { get; set; }
        public int MaximoDeDatos { get; set; }
        public bool IrALaUltimaPagina { get; set; }
    }
}
