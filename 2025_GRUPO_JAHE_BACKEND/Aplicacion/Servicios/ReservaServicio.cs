using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Enumeraciones;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class ReservaServicio : IReservaServicio
    {
        private readonly IReservaRepositorio _repositorio;

        private readonly ITransactionMethods _transaction;



        public ReservaServicio(IReservaRepositorio reservaRepositorio,
                                ITransactionMethods transaction)
        {
            this._repositorio = reservaRepositorio;
            this._transaction = transaction;
        }

        public async Task<List<string>> RealizarReserva(List<ReservaDTO> reservasDTO, ClienteDTO clienteDTO)
        {
            try
            {
                await this._transaction.BeginTransactionAsync();

                // reconocer cliente, si no existe crear uno nuevo
                var cliente = await this._repositorio.VerCliente(clienteDTO.Email);
                if (cliente == null)
                {
                    cliente = new Cliente
                    {
                        Nombre = clienteDTO.Nombre,
                        Apellidos = clienteDTO.Apellidos,
                        Email = clienteDTO.Email,
                        TarjetaDePago = clienteDTO.TarjetaDePago
                    };
                }

                decimal montoTotal = 0; 

                // calcular el monto total
                foreach (ReservaDTO reservaDTO in reservasDTO)
                {
                    var habitacion = await this._repositorio.VerHabitacion(reservaDTO.IdHabitacion);
                    TimeSpan diferencia = reservaDTO.FechaSalida - reservaDTO.FechaLlegada;
                    int cantidadDias = (int)diferencia.TotalDays;
                    montoTotal += (decimal)habitacion.TipoDeHabitacion.TarifaDiaria * cantidadDias;
                }

                // crear transaccion
                var transaccion = await this._repositorio.RealizarTransaccion(montoTotal, "test");
                if (transaccion == null)
                {
                    await this._transaction.RollbackAsync();
                    return null;
                }

                // insertar las reservas
                List<string> idsReservas = new List<string>();

                foreach (ReservaDTO reservaDTO in reservasDTO)
                {
                    var habitacion = await this._repositorio.VerHabitacion(reservaDTO.IdHabitacion);

                    if (!await this._repositorio.CambiarEstadoHabitacion(habitacion.IdHabitacion, EstadoDeHabitacion.RESERVADA.ToString()))
                    {
                        await this._transaction.RollbackAsync();
                        return null;
                    }

                    var reserva = new Reserva
                    {
                        FechaLlegada = reservaDTO.FechaLlegada,
                        FechaSalida = reservaDTO.FechaSalida,
                        Estado = EstadoDeReserva.CONFIRMADA.ToString(),
                        Activo = true,
                        Cliente = cliente,
                        Habitacion = habitacion,
                        Transaccion = transaccion
                    };

                    string idReserva = await this._repositorio.RealizarReserva(reserva);

                    idsReservas.Add(idReserva);

                }

                await this._transaction.CommitAsync();
                return idsReservas;
            }
            catch (Exception ex)
            {
                await this._transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<HabitacionDTO> VerHabitacionDisponible(ReservaDTO reservaDTO)
        {
            var habitacion = await _repositorio.VerHabitacionDisponible(reservaDTO.IdTipoDeHabitacion, reservaDTO.FechaLlegada, reservaDTO.FechaSalida);

            if(habitacion == null)
            {
                return null;
            }


            return new HabitacionDTO
            {
                IdHabitacion = habitacion.IdHabitacion,
                IdTipoDeHabitacion = habitacion.IdTipoDeHabitacion,
                Numero = habitacion.Numero,
                TipoDeHabitacion = new TipoDeHabitacionDTO
                {
                    IdTipoDeHabitacion = habitacion.TipoDeHabitacion.IdTipoDeHabitacion,
                    Nombre = habitacion.TipoDeHabitacion.Nombre,
                    TarifaDiaria = habitacion.TipoDeHabitacion.TarifaDiaria 
                },
            };
        }

        public async Task<bool> CambiarEstadoHabitacion(int idHabitacion, string nuevoEstado)
        {
            return await this._repositorio.CambiarEstadoHabitacion(idHabitacion, nuevoEstado);
        }

        public async Task<List<TipoDeHabitacionDTO>> VerTiposDeHabitacion()
        {
            var tiposDeHabitacionDTO = new List<TipoDeHabitacionDTO>();

            tiposDeHabitacionDTO = (await this._repositorio.VerTiposDeHabitacion()).Select(t => new TipoDeHabitacionDTO
            {
                IdTipoDeHabitacion = t.IdTipoDeHabitacion,
                Nombre = t.Nombre,
                TarifaDiaria = t.TarifaDiaria
            }).ToList();

            return tiposDeHabitacionDTO;
        }
    }
}
