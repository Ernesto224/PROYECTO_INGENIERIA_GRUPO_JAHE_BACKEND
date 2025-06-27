using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporadaController : Controller
    {
        private readonly ITemporadaServicio _temporadaServicio;

        public TemporadaController(ITemporadaServicio temporadaServicio)
        {
            this._temporadaServicio = temporadaServicio;
        }

        [HttpPut]
        [Route("ModificarTemporadaAlta")]
        public async Task<ActionResult<RespuestaDTO<TemporadaDTO>>> Put(TemporadaDTO temporadaDTO)
        {
            try
            {
                RespuestaDTO<TemporadaDTO> respuesta = await this._temporadaServicio.ModificarTemporadaAlta(temporadaDTO);
                if (respuesta == null)
                {
                    return NotFound("No se pudo modificar la temporada alta.");
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("ObtenerTemporadaAlta")]
        public async Task<ActionResult<TemporadaDTO>> Get()
        {
            try
            {
                TemporadaDTO temporada = await this._temporadaServicio.ObtenerTemporadaAlta();
                if (temporada == null)
                {
                    return NotFound("No se encontró la temporada alta.");
                }
                return Ok(temporada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
