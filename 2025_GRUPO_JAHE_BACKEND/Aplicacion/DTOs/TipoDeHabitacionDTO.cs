using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class TipoDeHabitacionDTO
    {
        public int IdTipoDeHabitacion { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal TarifaDiaria { get; set; }

        public ImagenDTO? Imagen { get; set; }
    }
}
