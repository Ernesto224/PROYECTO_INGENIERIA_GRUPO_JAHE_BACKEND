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
using Dominio.Servicios_de_Dominio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aplicacion.Servicios
{
    public class ReservaServicio : IReservaServicio
    {
        private readonly IReservaRepositorio _repositorioReserva;

        private readonly ITemporadaRepositorio _repositorioTemporada;

        private readonly IOfertaRepositorio ofertaRepositorio;

        private readonly IServicioEmail _servicioEmail;

        private readonly ITransactionMethods _transaction;


        public ReservaServicio(IReservaRepositorio reservaRepositorio,
                                ITemporadaRepositorio temporadaRepositorio,
                                ITransactionMethods transaction,
                                IServicioEmail servicioEmail,
                                IOfertaRepositorio ofertaRepositorio)
        {
            this._repositorioReserva = reservaRepositorio;
            this._transaction = transaction;
            this._servicioEmail = servicioEmail;
            this._repositorioTemporada = temporadaRepositorio;
            this.ofertaRepositorio = ofertaRepositorio;
        }

        public async Task<List<string>> RealizarReserva(List<ReservaDTO> reservasDTO, ClienteDTO clienteDTO)
        {
            try
            {
                await this._transaction.BeginTransactionAsync();

                // reconocer cliente, si no existe crear uno nuevo
                var cliente = await this._repositorioReserva.VerCliente(clienteDTO.Email);
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


                // calcular el monto total
                decimal montoTotal = 0;
                var calculador = new CalcularPrecioService();
                foreach (var reservaDTO in reservasDTO)
                {
                    var habitacion = await this._repositorioReserva.VerHabitacion(reservaDTO.IdHabitacion);
                    var reserva = new Reserva();
                    decimal montoBase = reserva.CalcularMontoBase(habitacion, reservaDTO.FechaLlegada, reservaDTO.FechaSalida);
                    var temporada = await _repositorioTemporada.ObtenerTemporadaPorFecha(reservaDTO.FechaLlegada, reservaDTO.FechaSalida);

                    if (temporada != null)
                    {
                        montoTotal += calculador.AplicarTemporada(montoBase, temporada);
                    }

                    else
                    {
                        montoTotal = montoBase; // No se aplica temporada
                    }

                        
                }


                // crear transaccion
                var transaccion = Transaccion.Crear(montoTotal, "Reserva Hotel");
                await _repositorioReserva.RealizarTransaccion(transaccion);

                // insertar las reservas
                List<string> idsReservas = new List<string>();

                List<Reserva> reservasRealizadas = new List<Reserva>();

                foreach (ReservaDTO reservaDTO in reservasDTO)
                {
                    var habitacion = await this._repositorioReserva.VerHabitacion(reservaDTO.IdHabitacion);

                    if (!await this._repositorioReserva.CambiarEstadoHabitacion(habitacion.IdHabitacion, EstadoDeHabitacion.RESERVADA.ToString()))
                    {
                        await this._transaction.RollbackAsync();
                        return null;
                    }

                    var reserva = new Reserva
                    {
                        FechaLlegada = reservaDTO.FechaLlegada,
                        FechaSalida = reservaDTO.FechaSalida,
                        Estado = EstadoDeReserva.CONFIRMADA.ToString(),
                        Activa = true,
                        Cliente = cliente,
                        Habitacion = habitacion,
                        Transaccion = transaccion
                    };

                    string idReserva = await this._repositorioReserva.RealizarReserva(reserva);

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
            var habitacion = await _repositorioReserva.VerHabitacionDisponible(reservaDTO.IdTipoDeHabitacion, reservaDTO.FechaLlegada, reservaDTO.FechaSalida);

            if(habitacion == null)
            {
                return null;
            }

            var precioHabitacion = habitacion.TipoDeHabitacion.TarifaDiaria;

            var temporada = await _repositorioTemporada.ObtenerTemporadaPorFecha(reservaDTO.FechaLlegada, reservaDTO.FechaSalida);

            if(temporada != null)
            {
                var calculador = new CalcularPrecioService();
                precioHabitacion = calculador.AplicarTemporada(habitacion.TipoDeHabitacion.TarifaDiaria, temporada);
                Console.WriteLine("SE APLICO LA TEMPORADA " + precioHabitacion);
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
                    TarifaDiaria = precioHabitacion
                },
            };
        }

        public async Task<bool> CambiarEstadoHabitacion(int idHabitacion, string nuevoEstado)
        {
            return await this._repositorioReserva.CambiarEstadoHabitacion(idHabitacion, nuevoEstado);
        }

        public async Task<IEnumerable<AlternativaDeReservaDTO>> VerAlternativasDisponibles(ReservaDTO reservaDTO)
        {
            var alternativasDisponibles = await this._repositorioReserva.VerAlternativasDisponibles(
                reservaDTO.IdTipoDeHabitacion,
                reservaDTO.FechaLlegada,
                reservaDTO.FechaSalida
            );

            var alternativasDisponiblesDTO = alternativasDisponibles.Select(al => new AlternativaDeReservaDTO
            {
                TipoDeHabitacionDTO = new TipoDeHabitacionDTO
                {
                    IdTipoDeHabitacion = al.tipo.IdTipoDeHabitacion,
                    Nombre = al.tipo.Nombre,
                    TarifaDiaria = al.tipo.TarifaDiaria,
                },
                Inicio = al.inicio,
                Fin = al.fin
            }).ToList();

            return alternativasDisponiblesDTO;
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
        public async Task<RespuestaConsultaDTO<DatoReservaDTO>> ListarReservaciones(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            var resultado = await _repositorioReserva.ListarReservaciones(numeroDePagina, maximoDeDatos, irALaUltimaPagina);

            return new RespuestaConsultaDTO<DatoReservaDTO>
            {
                Lista = resultado.reservas.Select(item => new DatoReservaDTO
                {
                    Fecha = DateTime.Now,
                    IdReserva = item.IdReserva,
                    Nombre = item.Cliente?.Nombre,
                    Apellidos = item.Cliente?.Apellidos,
                    Email = item.Cliente?.Email,
                    Tarjeta = item.Cliente?.TarjetaDePago,
                    Transaccion = item.Transaccion?.Descripcion,
                    FechaLlegada = item.FechaLlegada,
                    FechaSalida = item.FechaSalida,
                    Tipo = item.Habitacion?.TipoDeHabitacion?.Nombre
                }),
                TotalRegistros = resultado.totalRegistros,
                PaginaActual = resultado.paginaActual,
                MaximoPorPagina = maximoDeDatos
            };
        }

        public async Task<bool> EliminarReserva(string idReserva)
        {
            var resultado = await _repositorioReserva.EliminarReserva(idReserva);

            if (!resultado)
                throw new InvalidOperationException("No se pudo eliminar la reserva.");

            return resultado;
        }

        public async Task<DatoReservaDTO> DetalleReservacion(string idReserva)
        {
            var resultado = await _repositorioReserva.DetalleReservacion(idReserva);
            if (resultado != null)
            {
                return new DatoReservaDTO
                {
                    Fecha = DateTime.Now,
                    IdReserva = resultado.IdReserva,
                    Nombre = resultado.Cliente?.Nombre,
                    Apellidos = resultado.Cliente?.Apellidos,
                    Email = resultado.Cliente?.Email,
                    Tarjeta = resultado.Cliente?.TarjetaDePago,
                    Transaccion = resultado.Transaccion?.Descripcion,
                    FechaLlegada = resultado.FechaLlegada,
                    FechaSalida = resultado.FechaSalida,
                    Tipo = resultado.Habitacion?.TipoDeHabitacion?.Nombre
                };
            }
            return null;
        }
    }
}
