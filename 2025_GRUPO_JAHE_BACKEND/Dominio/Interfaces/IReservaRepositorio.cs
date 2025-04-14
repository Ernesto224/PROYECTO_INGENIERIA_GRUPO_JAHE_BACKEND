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

        public Task<Habitacion> VerHabitacion(int idHabitacion);

        public Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo);

        public Task<Transaccion> RealizarTransaccion(decimal monto, string descripcion);

        public Task<Habitacion> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida);

        public Task<List<TipoDeHabitacion>> VerTiposDeHabitacion();

        public Task<Cliente> VerCliente(string email);

        public Task<List<Oferta>> VerOfertasAplicables(int idTipoDeHabitacion, DateTime fechaLlegada, DateTime fechaSalida);

    }
}
