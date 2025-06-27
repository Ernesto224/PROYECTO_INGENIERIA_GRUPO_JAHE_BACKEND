using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : Controller
    {
        private readonly IReservaServicio reservaServicio;

        public ReservaController(IReservaServicio reservaServicio)
        {
            this.reservaServicio = reservaServicio;
        }

        [HttpPost]
        public async Task<ActionResult<List<string>>> AgregarReserva(ReservaCompletaDTO reservaCompletaDTO)
        {
            if (reservaCompletaDTO.ReservasDTO == null)
            {
                return BadRequest("Reservas no puede ser nulo");
            }


            var idsReservas = await reservaServicio.RealizarReserva(reservaCompletaDTO.ReservasDTO, reservaCompletaDTO.ClienteDTO);

            if (idsReservas != null)
            {
                return Ok(idsReservas);
            }
            else
            {
                return BadRequest("Error al realizar la reserva");
            }
        }

        [HttpGet]
        public async Task<ActionResult<HabitacionDTO>> VerHabitacionDisponible(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            var resultado = await reservaServicio.VerHabitacionDisponible(new ReservaDTO
            {
                IdTipoDeHabitacion = idTipoHabitacion,
                FechaLlegada = fechaLlegada,
                FechaSalida = fechaSalida
            });

            if (resultado != null)
            {
                return Ok(resultado);
            }
            else
            {
                return Ok("No hay habitaciones disponibles");
            }
        }

        [HttpGet]
        [Route("alternativas")]
        public async Task<ActionResult<AlternativaDeReservaDTO>> VerAlternativasDisponibles(int idTipoHabitacion, DateTime fechaLlegada, DateTime fechaSalida)
        {
            var resultado = await reservaServicio.VerAlternativasDisponibles(new ReservaDTO
            {
                IdTipoDeHabitacion = idTipoHabitacion,
                FechaLlegada = fechaLlegada,
                FechaSalida = fechaSalida
            });

            if (resultado != null)
            {
                return Ok(resultado);
            }
            else
            {
                return Ok("No hay alternativas disponibles");
            }
        }

        [HttpPut]
        public async Task<ActionResult<bool>> CambiarEstadoHabitacion(int idHabitacion, string nuevoEstado)
        {
            var resultado = await this.reservaServicio.CambiarEstadoHabitacion(idHabitacion, nuevoEstado);

            if (resultado == true)
            {
                return Ok(resultado);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("ListaReservaciones")]
        public async Task<ActionResult<RespuestaConsultaDTO<DatoReservaDTO>>> ListarReservaciones(ConsultaReservacionesParametrosDTO parametros)
        {
            try
            {
                var resultado = await reservaServicio.ListarReservaciones(parametros.NumeroDePagina, parametros.MaximoDeDatos, parametros.IrALaUltimaPagina);

                if (resultado == null || !resultado.Lista.Any())
                    return NotFound("No hay reservas registradas");

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        [HttpDelete("{idReserva}")]
        public async Task<ActionResult<bool>> EliminarReserva(string idReserva)
        {
            try
            {
                var resultado = await reservaServicio.EliminarReserva(idReserva);

                if (resultado)
                    return Ok(true);
                else
                    return NotFound("Reserva no encontrada o no pudo ser eliminada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
        [HttpGet("DetalleReservacion/{idReserva}")]
        public async Task<ActionResult<DatoReservaDTO>> DetalleReservacion(string idReserva)
        {
            try
            {
                if (string.IsNullOrEmpty(idReserva))
                    return BadRequest("El ID de reserva no puede ser nulo o vacío");
                var resultado = await reservaServicio.DetalleReservacion(idReserva);
                if (resultado == null)
                    return NotFound("No se encontró la reserva solicitada");
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }
    }
}
