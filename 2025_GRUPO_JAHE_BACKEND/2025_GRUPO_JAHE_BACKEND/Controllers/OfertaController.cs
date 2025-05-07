using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfertaController : Controller
    {
        private readonly IOfertaServicio _ofertaServicio;

        public OfertaController(IOfertaServicio ofertaServicio)
        {
            this._ofertaServicio = ofertaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<OfertaDTO>>> Get()
        {
            try
            {
                var ofertaDTO = await this._ofertaServicio.VerOfertas();

                if (ofertaDTO == null)
                {
                    return NotFound("No se encontraron ofertas.");
                }
                return Ok(ofertaDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OfertaDTO>> Post([FromBody] OfertaDTO ofertaDTO)
        {
            try
            {
                var ofertaCreada = await this._ofertaServicio.CrearOferta(ofertaDTO);

                if (ofertaCreada == null)
                {
                    return NotFound("No se pudo crear la oferta.");
                }
                return Ok(ofertaCreada);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
