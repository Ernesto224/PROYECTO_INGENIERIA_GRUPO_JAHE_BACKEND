using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly IHabitacionServicio _habitacionServicio;

        public HabitacionController (IHabitacionServicio habitacionServicio)
        {
            this._habitacionServicio = habitacionServicio;
        }

        // GET: api/<FacilidadController>
        [Authorize]
        [HttpPost("ConsultaHabitaciones")]
        public async Task<ActionResult<RespuestaConsultaDTO<HabitacionConsultaDTO>>> Get(ConsultaHabitacionesParametrosDTO parametrosDTO)
        {
            try
            {
                var resultadoConsultaDTO = await this._habitacionServicio.ConsultarDisponibilidadDeHabitaciones(parametrosDTO.IdTiposHabitacion
                    , parametrosDTO.FechaLlegada, parametrosDTO.FechaSalida, parametrosDTO.NumeroDePagina
                    , parametrosDTO.MaximoDeDatos, parametrosDTO.IrALaUltimaPagina);

                if (resultadoConsultaDTO == null)
                    return NotFound("No se encontraron datos.");

                return Ok(resultadoConsultaDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        //[Authorize]
        [HttpPost("ConsultaHabitacionesHoy")]
        public async Task<ActionResult<RespuestaConsultaDTO<HabitacionConEstadoDTO>>> GetHabitacionesHoy(ConsultaPaginadaBaseDTO parametros)
        {
            try
            {
                var resultadoConsultaDTO = await this._habitacionServicio.ConsultarDisponibilidadDeHabitacionesHoy(parametros.NumeroDePagina, parametros.MaximoDeDatos, parametros.IrALaUltimaPagina);

                if (resultadoConsultaDTO == null)
                    return NotFound("No se encontraron datos.");

                return Ok(resultadoConsultaDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
