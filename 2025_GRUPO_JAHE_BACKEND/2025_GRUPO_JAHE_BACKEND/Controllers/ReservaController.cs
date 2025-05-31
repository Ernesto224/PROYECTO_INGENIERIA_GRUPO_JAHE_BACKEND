using Aplicacion.DTOs;
using Aplicacion.Interfaces;
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

    }
}
