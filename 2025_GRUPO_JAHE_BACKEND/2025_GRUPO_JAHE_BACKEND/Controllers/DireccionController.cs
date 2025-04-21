using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IDireccionServicio _direccionServicio;
        public DireccionController(IDireccionServicio direccionServicio)
        {
            this._direccionServicio = direccionServicio;
        }
        [HttpGet]
        public async Task<ActionResult<DireccionDTO>> Get()
        {
            try
            {
                var direccionDTO = await _direccionServicio.VerDatosDireccion();
                return Ok(direccionDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en al base de datos");
            }
        }
        [HttpPut]
        public async Task<ActionResult<DireccionDTO>> CambiarTextoComoLlegar(DireccionDTO direccionDTO)
        {
            var direccionActualizada = await _direccionServicio.CambiarTextoComoLlegar(direccionDTO);
            if (direccionActualizada != null)
            {
                return Ok(direccionActualizada);
            }
            return BadRequest("Hubo un error en el servicio");
        }
    }
}
