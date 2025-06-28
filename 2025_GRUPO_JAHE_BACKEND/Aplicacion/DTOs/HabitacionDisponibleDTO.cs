using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class HabitacionDisponibleDTO
    {
        public HabitacionDTO habitacionDTO { get; set; }

        public OfertaDTO ofertaDTO { get; set; }

        public float precioTotal { get; set; }
    }
}
