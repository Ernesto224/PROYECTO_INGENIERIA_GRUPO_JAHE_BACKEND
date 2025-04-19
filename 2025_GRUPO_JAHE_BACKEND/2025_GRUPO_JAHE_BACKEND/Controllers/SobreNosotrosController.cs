using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SobreNosotrosController : ControllerBase
    {
        private readonly ISobreNosotrosServicio _sobreNosotrosServicio;

        public SobreNosotrosController(ISobreNosotrosServicio sobreNosotrosServicio)
        {
            _sobreNosotrosServicio = sobreNosotrosServicio;
        }

        [HttpGet]
        public async Task<ActionResult<SobreNosotrosDTO>> Get()
        {
            try
            {
                var sobreNosotrosDTO = await _sobreNosotrosServicio.VerDatosSobreNosotros();
                return Ok(sobreNosotrosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en al base de datos");
            }
        }
        [HttpPut]
        public async Task<ActionResult<SobreNosotrosDTO>> CambiarTextoSobreNosotros(SobreNosotrosDTO sobreNosotrosDTO)
        {
            var sobreNosotrosActualizado = await _sobreNosotrosServicio.CambiarTextoSobreNosotros(sobreNosotrosDTO);
            if (sobreNosotrosActualizado != null)
            {
                return Ok(sobreNosotrosActualizado);
            }
            return BadRequest("Hubo un error en el servicio");
        }
        [HttpPut("CambiarImagenes")]
        public async Task<ActionResult<SobreNosotrosDTO>>CambiarImagenGaleriaSobreNosotros(SobreNosotrosDTO sobreNosotrosDTO)
        {
            var _sobreNosotrosDTO = await _sobreNosotrosServicio.CambiarImagenGaleriaSobreNosotros(sobreNosotrosDTO);
            return _sobreNosotrosDTO != null? Ok(_sobreNosotrosDTO) : StatusCode(500, "Error al cambiar las imagenes");
        }

    }
}
