using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IEstadoHabitacionServicio
    {
        public Task<IEnumerable<EstadoHabitacionDTO>> verHabitaciones();

        public Task<object> ActualizarEstadoDeHabitacion(ActualizarEstadoHabitacionDTO actualizarEstadoHabitacionDTO);

    }
}
