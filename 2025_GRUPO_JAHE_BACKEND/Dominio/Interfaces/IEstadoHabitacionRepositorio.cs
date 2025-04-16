using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IEstadoHabitacionRepositorio
    {

        public Task<IEnumerable<Habitacion>> verHabitaciones();

        public Task<object> ActualizarEstadoDeHabitacion(int idHabitacion, string nuevoEstado);

    }
}
