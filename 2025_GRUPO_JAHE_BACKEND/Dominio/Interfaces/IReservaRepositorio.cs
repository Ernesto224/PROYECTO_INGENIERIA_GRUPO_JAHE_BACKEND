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
        public Task<string> RealizarReserva(Reserva reserva);

        public Task<Habitacion> VerHabitacion(int idHabitacion);

        public Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo);

        public Task<Transaccion> RealizarTransaccion(Transaccion transaccion);

        public Task<Habitacion> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida);

        public Task<IEnumerable<(TipoDeHabitacion tipo, DateTime inicio, DateTime fin)>> VerAlternativasDisponibles(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida);

        public Task<Cliente> VerCliente(string email);

        public Task<List<Oferta>> VerOfertasAplicables(int idTipoDeHabitacion, DateTime fechaLlegada, DateTime fechaSalida);
        public Task<(IEnumerable<Reserva> reservas, int totalRegistros, int paginaActual)> ListarReservaciones(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina);
        public Task<bool> EliminarReserva(string idReserva);
        public Task<Reserva> DetalleReservacion(string idReserva);
    }
}
