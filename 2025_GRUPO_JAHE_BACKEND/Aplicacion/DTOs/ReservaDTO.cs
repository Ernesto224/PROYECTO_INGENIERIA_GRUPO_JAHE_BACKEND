using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Aplicacion.DTOs
{
    public class ReservaDTO
    {

        public DateTime FechaLlegada { get; set; }

        public DateTime FechaSalida { get; set; }

        public int IdTipoDeHabitacion { get; set; }

    }
}
