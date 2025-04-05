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
        public async Task<ActionResult<bool>> AgregarReserva(ReservaCompletaDTO reservaCompletaDTO)
        {
            if (reservaCompletaDTO.ReservaDTO == null)
            {
                return BadRequest("Reserva no puede ser nulo");
            }


            var resultado = await reservaServicio.RealizarReserva(reservaCompletaDTO.ReservaDTO, reservaCompletaDTO.ClienteDTO, reservaCompletaDTO.HabitacionDTO);

            if (resultado)
            {
                return Ok(resultado);
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
                return NotFound("No hay habitaciones disponibles");
            }
        }
    }
}
