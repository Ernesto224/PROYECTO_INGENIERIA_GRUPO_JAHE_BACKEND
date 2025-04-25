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

        private readonly IServicioEmail _servicioEmail;

        private readonly ITransactionMethods _transaction;



        public ReservaServicio(IReservaRepositorio reservaRepositorio,
                                ITransactionMethods transaction,
                                IServicioEmail servicioEmail)
        {
            this._repositorio = reservaRepositorio;
            this._transaction = transaction;
            this._servicioEmail = servicioEmail;
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

                List<Reserva> reservasRealizadas = new List<Reserva>();

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

                    reservasRealizadas.Add(reserva);

                    idsReservas.Add(idReserva);

                }

                

                await this._transaction.CommitAsync();

                // Si todo sale bien se le envia el correo al cliente
                // Usando el SG

                this.EnviarCorreo(reservasRealizadas, clienteDTO, idsReservas, montoTotal);


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

        private async void EnviarCorreo(List<Reserva> reservas, ClienteDTO clienteDTO, List<string>idsReservas, decimal montoTotal)
        {
            string asuntoEmail = $"Confirmación de Reserva - Hotel Jade";

            StringBuilder mensajeEmail = new StringBuilder();
            mensajeEmail.AppendLine($"<h2 style='color: #2a5caa;'>¡Gracias por su reserva, {clienteDTO.Nombre} {clienteDTO.Apellidos}!</h2>");
            mensajeEmail.AppendLine("<p>Su reserva en el Hotel Jade ha sido confirmada. A continuación encontrará los detalles:</p>");
            mensajeEmail.AppendLine("<br/>");

            mensajeEmail.AppendLine("<h3 style='color: #2a5caa;'>Detalles de la Reserva</h3>");
            mensajeEmail.AppendLine("<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; width: 100%;'>");
            mensajeEmail.AppendLine("<tr style='background-color: #f2f2f2;'><th>N° Reserva</th><th>Habitación</th><th>Tipo</th><th>Check-in</th><th>Check-out</th><th>Noches</th></tr>");

            foreach (Reserva reservaDTO in reservas)
            {
                var habitacion = reservaDTO.Habitacion;
                TimeSpan diferencia = reservaDTO.FechaSalida - reservaDTO.FechaLlegada;
                int cantidadDias = (int)diferencia.TotalDays;

                mensajeEmail.AppendLine($"<tr>");
                mensajeEmail.AppendLine($"<td>{idsReservas[reservas.IndexOf(reservaDTO)]}</td>");
                mensajeEmail.AppendLine($"<td>{habitacion?.Numero}</td>");
                mensajeEmail.AppendLine($"<td>{habitacion?.TipoDeHabitacion.Nombre}</td>");
                mensajeEmail.AppendLine($"<td>{reservaDTO.FechaLlegada.ToString("dd/MM/yyyy")}</td>");
                mensajeEmail.AppendLine($"<td>{reservaDTO.FechaSalida.ToString("dd/MM/yyyy")}</td>");
                mensajeEmail.AppendLine($"<td>{cantidadDias}</td>");
                mensajeEmail.AppendLine($"</tr>");
            }

            mensajeEmail.AppendLine("</table>");
            mensajeEmail.AppendLine("<br/>");

            mensajeEmail.AppendLine($"<h3 style='color: #2a5caa;'>Total Pagado: {montoTotal.ToString("C")}</h3>");
            mensajeEmail.AppendLine("<br/>");

            mensajeEmail.AppendLine("<h3 style='color: #2a5caa;'>Información Adicional</h3>");
            mensajeEmail.AppendLine("<ul>");
            mensajeEmail.AppendLine("<li>Check-in: A partir de las 14:00 hrs</li>");
            mensajeEmail.AppendLine("<li>Check-out: Antes de las 12:00 hrs</li>");
            mensajeEmail.AppendLine("<li>Presentar este comprobante y documento de identidad al llegar</li>");
            mensajeEmail.AppendLine("</ul>");
            mensajeEmail.AppendLine("<br/>");

            mensajeEmail.AppendLine("<p>Si necesita hacer algún cambio en su reserva, por favor contáctenos a <a href='mailto:reservas@hoteljade.com'>reservas@hoteljade.com</a> o al teléfono +123 456 7890.</p>");
            mensajeEmail.AppendLine("<br/>");

            mensajeEmail.AppendLine("<p>¡Esperamos brindarle una excelente estadía!</p>");
            mensajeEmail.AppendLine("<p>Atentamente,</p>");
            mensajeEmail.AppendLine("<p><strong>Equipo de Reservas<br/>Hotel Jade</strong></p>");

            await this._servicioEmail.enviarEmail(clienteDTO.Email, asuntoEmail, mensajeEmail.ToString());
        }
    }
}
