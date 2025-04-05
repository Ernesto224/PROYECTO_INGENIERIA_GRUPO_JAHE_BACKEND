using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface IReservaRepositorio
    {
        public Task<bool> RealizarReserva(Reserva reserva);

        public Task<bool> CambiarEstadoHabitacion(Habitacion habitacion, string estadoNuevo);

        public Task<Transaccion> RealizarTransaccion(decimal monto, string descripcion);

        public Task<Habitacion> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida);

    }
}
