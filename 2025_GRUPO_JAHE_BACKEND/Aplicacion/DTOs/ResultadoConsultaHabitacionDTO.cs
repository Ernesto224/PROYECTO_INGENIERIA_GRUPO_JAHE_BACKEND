using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ResultadoConsultaHabitacionDTO
    {
        public IEnumerable<HabitacionConsultaDTO> Habitaciones { get; set; }
        public int TotalRegistros { get; set; }
        public int PaginaActual { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / MaximoPorPagina);
        public int MaximoPorPagina { get; set; }
    }
}
