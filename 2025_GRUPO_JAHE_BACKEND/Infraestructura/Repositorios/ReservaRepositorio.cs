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

        public async Task<string> RealizarReserva(Reserva reserva)
        {
            var resultado = await _contexto.Reservas.AddAsync(reserva);
            //await _contexto.SaveChangesAsync();

            string idReserva = resultado.Entity.IdReserva.ToString();

            return idReserva;
        }

        public async Task<Habitacion> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {

            var HabitacionDisponible = await _contexto.Habitaciones
                .Include(h => h.TipoDeHabitacion)
                .Where(h => h.IdTipoDeHabitacion == idTipoHabitacion &&
                            (h.Estado != EstadoDeHabitacion.NO_DISP.ToString() && h.Estado != EstadoDeHabitacion.OCUPADA.ToString()))
                .Where(h => !_contexto.Reservas.Any(r =>
                    r.IdHabitacion == h.IdHabitacion &&
                    r.Activa &&
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

                if(await this.CambiarEstadoHabitacion(HabitacionDisponible.IdHabitacion, EstadoDeHabitacion.OCUPADA.ToString()))
                {
                    return HabitacionDisponible;
                }
                else
                {
                    return null;
                }
            }

        }

        public async Task<Habitacion> VerHabitacion(int idHabitacion)
        {
            var habitacion = await _contexto.Habitaciones
                .Include(h => h.TipoDeHabitacion)
                .FirstOrDefaultAsync(h => h.IdHabitacion == idHabitacion);

            if (habitacion == null)
            {
                return null;
            }
            else
            {
                return habitacion;
            }
        }

        public async Task<Transaccion> RealizarTransaccion(Transaccion transaccion)
        {
            var resultado = await _contexto.Transacciones.AddAsync(transaccion);

            return resultado.Entity;
        }

        public async Task<decimal> VerMontoPorTipoHabitacion(int idTipoHabitacion)
        {
            var tipoDeHabitacion = await this._contexto.TipoDeHabitaciones.FirstOrDefaultAsync(t => t.IdTipoDeHabitacion == idTipoHabitacion);

            return 0;
        }

        public async Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo)
        {
            var habitacion = await this.VerHabitacion(idHabitacion);

            habitacion.Estado = estadoNuevo;

            habitacion.FechaEstado = DateTime.Now;

            await this._contexto.SaveChangesAsync();

            return true;
        }

        public Task<Cliente> VerCliente(string email)
        {
            var cliente = _contexto.Clientes.FirstOrDefaultAsync(c => c.Email == email);
            return cliente;
        }

        public async Task<List<Oferta>> VerOfertasAplicables(int idTipoDeHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            var ofertasAplicables = await _contexto.Ofertas
                .Where(o => o.Activa &&
                   o.IdTipoDeHabitacion == idTipoDeHabitacion &&
                   !(fechaSalida < o.FechaInicio || fechaLlegada > o.FechaFinal))
                .ToListAsync();

            return ofertasAplicables;
        }

        public async Task<IEnumerable<(TipoDeHabitacion tipo, DateTime inicio, DateTime fin)>> VerAlternativasDisponibles(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            try
            {
                int cantidadDeDias = (fechaSalida.Date - fechaLlegada.Date).Days;

                var resultTemp = await _contexto.Habitaciones
                    .Include(h => h.TipoDeHabitacion)
                    .Where(h => h.IdTipoDeHabitacion != idTipoHabitacion &&
                                h.Estado != EstadoDeHabitacion.NO_DISP.ToString() &&
                                h.Estado != EstadoDeHabitacion.OCUPADA.ToString())
                    .Where(h => !_contexto.Reservas.Any(r =>
                        r.IdHabitacion == h.IdHabitacion &&
                        r.Activa &&
                        r.Estado != EstadoDeReserva.CANCELADA.ToString() &&
                        r.FechaLlegada < fechaSalida &&
                        r.FechaSalida > fechaLlegada))
                    .Take(3)
                    .Select(h => new
                    {
                        Tipo = h.TipoDeHabitacion,
                        Inicio = fechaLlegada,
                        Fin = fechaSalida
                    })
                    .ToListAsync();

                var alternativasPorTipo = resultTemp
                    .Select(x => (x.Tipo, x.Inicio, x.Fin))
                    .ToList();

                var hoy = DateTime.Today;
                var fechaMaxima = hoy.AddMonths(2);

                var habitacionesMismoTipo = await _contexto.Habitaciones
                    .Include(h => h.TipoDeHabitacion)
                    .Where(h => h.IdTipoDeHabitacion == idTipoHabitacion &&
                                h.Estado != EstadoDeHabitacion.NO_DISP.ToString() &&
                                h.Estado != EstadoDeHabitacion.OCUPADA.ToString())
                    .ToListAsync();

                var alternativasPorFecha = new List<(TipoDeHabitacion tipo, DateTime inicio, DateTime fin)>();

                foreach (var h in habitacionesMismoTipo)
                {
                    for (int offset = 0; offset <= (fechaMaxima - hoy).Days - cantidadDeDias; offset++)
                    {
                        var inicio = hoy.AddDays(offset);
                        var fin = inicio.AddDays(cantidadDeDias);

                        bool estaDisponible = !_contexto.Reservas.Any(r =>
                            r.IdHabitacion == h.IdHabitacion &&
                            r.Activa &&
                            r.Estado != EstadoDeReserva.CANCELADA.ToString() &&
                            r.FechaLlegada < fin &&
                            r.FechaSalida > inicio);

                        if (estaDisponible)
                        {
                            alternativasPorFecha.Add((h.TipoDeHabitacion, inicio, fin));
                            break; // Solo una sugerencia por habitación
                        }

                        if (alternativasPorFecha.Count >= 3) break;
                    }

                    if (alternativasPorFecha.Count >= 3) break;
                }

                return alternativasPorTipo.Concat(alternativasPorFecha).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<(IEnumerable<Reserva> reservas, int totalRegistros, int paginaActual)> ListarReservaciones(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            var query = _contexto.Reservas
                .Where(r => r.Activa)
                .Include(r => r.Cliente)
                .Include(r => r.Habitacion).ThenInclude(h => h.TipoDeHabitacion)
                .Include(r => r.Transaccion);

            int totalRegistros = await query.CountAsync();
            int totalPaginas = (int)Math.Ceiling((double)totalRegistros / maximoDeDatos);
            int paginaActual = irALaUltimaPagina ? (totalPaginas == 0 ? 1 : totalPaginas) : numeroDePagina;

            var resultado = await query
                .Skip((paginaActual - 1) * maximoDeDatos)
                .Take(maximoDeDatos)
                .ToListAsync();

            return (resultado, totalRegistros, paginaActual);
        }

        public async Task<bool> EliminarReserva(string idReserva)
        {
            if (!Guid.TryParse(idReserva, out var guidReserva))
                throw new ArgumentException("El ID de reserva proporcionado no es válido.");

            var reserva = await _contexto.Reservas.FindAsync(guidReserva);
            if (reserva == null)
                throw new KeyNotFoundException($"No se encontró ninguna reserva con el ID {idReserva}.");

            reserva.Activa = false;
            var filasAfectadas = await _contexto.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        public async Task<Reserva> DetalleReservacion(string idReserva)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(idReserva))
                    throw new ArgumentException("El idReserva no puede estar vacío.", nameof(idReserva));

                if (!Guid.TryParse(idReserva, out Guid guidReserva))
                    throw new ArgumentException("El idReserva no tiene un formato válido de Guid.", nameof(idReserva));

                var reserva = await _contexto.Reservas
                    .Include(r => r.Cliente)
                    .Include(r => r.Habitacion).ThenInclude(h => h.TipoDeHabitacion)
                    .Include(r => r.Transaccion)
                    .FirstOrDefaultAsync(r => r.IdReserva == guidReserva && r.Activa);

                return reserva;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
