using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Infraestructura.Repositorios
{
    public class ReservaRepositorio : IReservaRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public ReservaRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> RealizarReserva(Reserva reserva)
        {
            var resultado = await _contexto.Reservas.AddAsync(reserva);
            await _contexto.SaveChangesAsync();

            return resultado.Entity != null;
        }

        public async Task<Habitacion> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {

            var HabitacionDisponible = await _contexto.Habitaciones
                .Include(h => h.TipoDeHabitacion)
                .Where(h => h.IdTipoDeHabitacion == idTipoHabitacion &&
                            h.Estado != EstadoDeHabitacion.NO_DISP.ToString())
                .Where(h => !_contexto.Reservas.Any(r =>
                    r.IdHabitacion == h.IdHabitacion &&
                    r.Activo &&
                    r.Estado != EstadoDeReserva.CANCELADA.ToString() &&
                    r.FechaLlegada.Date < fechaSalida.Date && 
                    r.FechaSalida.Date > fechaLlegada.Date)) 
                .FirstOrDefaultAsync();

            if (HabitacionDisponible == null)
            {
                return null;
            }
            else
            {
                HabitacionDisponible.Estado = "OCUPADA";
                return HabitacionDisponible;
            }

        }

        public async Task<Transaccion> RealizarTransaccion(decimal monto, string descripcion)
        {
            var transaccion = new Transaccion
            {
                Monto = monto,
                Descripcion = descripcion,
                Fecha = DateTime.Now
            };

            var resultado = await _contexto.Transacciones.AddAsync(transaccion);

            return resultado.Entity;
        }

        public async Task<decimal> VerMontoPorTipoHabitacion(int idTipoHabitacion)
        {
            var tipoDeHabitacion = await this._contexto.TipoDeHabitaciones.FirstOrDefaultAsync(t => t.IdTipoDeHabitacion == idTipoHabitacion);

            return 0;
        }

        public Task<bool> CambiarEstadoHabitacion(Habitacion habitacion, string estadoNuevo)
        {
            return this.CambiarEstadoHabitacion(habitacion, estadoNuevo);
        }
    }
}
