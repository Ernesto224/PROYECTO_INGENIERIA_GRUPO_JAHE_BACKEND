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

        public async Task<bool> RealizarReserva(ReservaDTO reservaDTO, ClienteDTO clienteDTO, HabitacionDTO habitacionDTO)
        {
            // recuperar email del cliente para verificar

            try
            {
                await this._transaction.BeginTransactionAsync();

                // 1. mapear la habitacion y cambiar su estado
                var habitacion = new Habitacion
                {
                    IdHabitacion = habitacionDTO.IdHabitacion,
                    IdTipoDeHabitacion = habitacionDTO.IdTipoDeHabitacion,
                    Estado = EstadoDeHabitacion.DISPONIBLE.ToString(),
                    TipoDeHabitacion = new TipoDeHabitacion
                    {
                        IdTipoDeHabitacion = habitacionDTO.TipoDeHabitacion.IdTipoDeHabitacion,
                        Nombre = habitacionDTO.TipoDeHabitacion.Nombre,
                        Descripcion = habitacionDTO.TipoDeHabitacion.Descripcion,
                        TarifaDiaria = habitacionDTO.TipoDeHabitacion.TarifaDiaria
                    }
                };

                if (await this._repositorio.CambiarEstadoHabitacion(habitacion, EstadoDeHabitacion.RESERVADA.ToString()) == false)
                {
                    await this._transaction.RollbackAsync();
                    return false;
                }

                // 2. calcular monto y luego ofertas
                TimeSpan diferencia = reservaDTO.FechaSalida - reservaDTO.FechaLlegada;
                int cantidadDias = (int)diferencia.TotalDays;
                
                decimal montoPagar = (decimal)(habitacion.TipoDeHabitacion.TarifaDiaria) * (cantidadDias);

                // 3. realizar transaccion en la tabla
                var transaccion = await this._repositorio.RealizarTransaccion(montoPagar, "descriptionTest");
                if (transaccion == null)
                {
                    await this._transaction.RollbackAsync();
                    return false;
                }

                // 4. crear los datos de la reserva con la habitacion generada y la transaccion realizada
                var reserva = new Reserva
                {
                    FechaLlegada = reservaDTO.FechaLlegada,
                    FechaSalida = reservaDTO.FechaSalida,
                    Estado = EstadoDeReserva.CONFIRMADA.ToString(),
                    Activo = true,
                    Cliente = new Cliente
                    {
                        Nombre = clienteDTO.Nombre,
                        Apellidos = clienteDTO.Apellidos,
                        Email =clienteDTO.Email,
                        TarjetaDePago = clienteDTO.TarjetaDePago
                    },
                    Habitacion = habitacion,
                    Transaccion = transaccion

                };

                // 5. guardar la reserva
                if (await this._repositorio.RealizarReserva(reserva) == false)
                {
                    await this._transaction.RollbackAsync();
                    return false;
                }
          

                // 6. commit
                await this._transaction.CommitAsync();

                return true;

            }
            catch (Exception ex)
            {
                await this._transaction.RollbackAsync();
                return false;
            }

        }

        public async Task<HabitacionDTO> VerHabitacionDisponible(ReservaDTO reservaDTO)
        {
            var habitacion = await _repositorio.VerHabitacionDisponible(reservaDTO.IdTipoDeHabitacion, reservaDTO.FechaLlegada, reservaDTO.FechaSalida);

            return new HabitacionDTO
            {
                IdHabitacion = habitacion.IdHabitacion,
                IdTipoDeHabitacion = habitacion.IdTipoDeHabitacion,
                Estado = habitacion.Estado,
                TipoDeHabitacion = new TipoDeHabitacionDTO
                {
                    IdTipoDeHabitacion = habitacion.TipoDeHabitacion.IdTipoDeHabitacion,
                    Nombre = habitacion.TipoDeHabitacion.Nombre,
                    Descripcion = habitacion.TipoDeHabitacion.Descripcion,
                    TarifaDiaria = habitacion.TipoDeHabitacion.TarifaDiaria
                }
            };
        }
    }
}
