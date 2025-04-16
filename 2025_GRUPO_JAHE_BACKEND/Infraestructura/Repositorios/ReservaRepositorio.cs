﻿using System;
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
                            (h.Estado != EstadoDeHabitacion.NO_DISP.ToString() && h.Estado != EstadoDeHabitacion.OCUPADA.ToString()))
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
                HabitacionDisponible.Estado = EstadoDeHabitacion.OCUPADA.ToString();
                await this._contexto.SaveChangesAsync();
                return HabitacionDisponible;
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

        public async Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo)
        {
            var habitacion = await this.VerHabitacion(idHabitacion);

            habitacion.Estado = estadoNuevo;

            await this._contexto.SaveChangesAsync();

            return true;
        }

        public Task<List<TipoDeHabitacion>> VerTiposDeHabitacion()
        {
            var tiposDeHabitacion = _contexto.TipoDeHabitaciones.ToListAsync();

            return tiposDeHabitacion;
        }

        public Task<Cliente> VerCliente(string email)
        {
            var cliente = _contexto.Clientes.FirstOrDefaultAsync(c => c.Email == email);
            return cliente;
        }

        public async Task<List<Oferta>> VerOfertasAplicables(int idTipoDeHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            var ofertasAplicables = await _contexto.Ofertas
                .Where(o => o.Activo &&
                   o.IdTipoDeHabitacion == idTipoDeHabitacion &&
                   !(fechaSalida < o.FechaInicio || fechaLlegada > o.FechaFinal))
                .ToListAsync();

            return ofertasAplicables;
        }
    }
}
