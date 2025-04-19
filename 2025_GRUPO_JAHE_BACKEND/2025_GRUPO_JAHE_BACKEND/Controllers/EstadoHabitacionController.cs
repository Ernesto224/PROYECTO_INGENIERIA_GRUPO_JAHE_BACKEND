using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoHabitacionController : ControllerBase
    {

        private readonly IEstadoHabitacionServicio _estadoHabitacionServicio;

        public EstadoHabitacionController(IEstadoHabitacionServicio estadoHabitacionServicio)
        {
            this._estadoHabitacionServicio = estadoHabitacionServicio;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoHabitacionDTO>>> Get()
        {
            try
            {
                var habitaciones = await this._estadoHabitacionServicio.verHabitaciones();

                if (habitaciones == null || !habitaciones.Any())
                    return NotFound("No se encontraron habitaciones.");

                return Ok(habitaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las habitaciones: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> ActualizarEstado([FromBody] ActualizarEstadoHabitacionDTO dto)
        {
            try
            {
                var resultado = await this._estadoHabitacionServicio.ActualizarEstadoDeHabitacion(dto);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado de la habitación: {ex.Message}");
            }
        }
    }
}
