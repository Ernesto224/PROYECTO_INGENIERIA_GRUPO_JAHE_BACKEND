using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class DatoReservaDTO
    {
        public DateTime? Fecha { get; set; }
        public Guid? IdReserva { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? Tarjeta { get; set; }
        public string? Transaccion { get; set; }
        public DateTime? FechaLlegada { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? Tipo { get; set; }

    }
}
