using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class OfertaDTO
    {
        public int IdOferta { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFinal { get; set; }

        public int Porcentaje { get; set; }

        public bool Activo { get; set; }

        public TipoDeHabitacionDTO? TipoDeHabitacion { get; set; }

        public ImagenDTO? Imagen { get; set; }
    }
}
