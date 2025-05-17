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

        [HttpPost]
        [Route("paginadas")]
        public async Task<ActionResult<RespuestaConsultaDTO<OfertaDTO>>> Post(ConsultaPaginadaBaseDTO parametros)
        {
            try
            {
                var ofertaDTO = await this._ofertaServicio.VerOfertas(parametros.NumeroDePagina, parametros.MaximoDeDatos, parametros.IrALaUltimaPagina);

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

        [HttpGet]
        public async Task<IEnumerable<OfertaDTO>> Get()
        {
            List<OfertaDTO> ofertasActivas = await this._ofertaServicio.VerOfertasActivas();

            if (ofertasActivas == null)
            {
                return null;
            }

            return ofertasActivas;

        }

        [HttpPost]
        public async Task<ActionResult<OfertaDTO>> Post([FromBody] OfertaDTO ofertaDTO)
        {
            try
            {
                RespuestaDTO<OfertaDTO> respuesta = await this._ofertaServicio.CrearOferta(ofertaDTO);

                if (respuesta == null)
                {
                    return NotFound("No se pudo crear la oferta.");
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{idOferta}")]
        public async Task<ActionResult<RespuestaDTO<OfertaDTO>>> Delete(int idOferta)
        {
            try
            {
                RespuestaDTO<OfertaDTO> respuesta = await this._ofertaServicio.EliminarOferta(idOferta);

                if (respuesta == null)
                {
                    return NotFound("No se pudo eliminar la oferta.");
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<RespuestaDTO<OfertaDTO>>> Put(OfertaModificarDTO ofertaModificarDTO)
        {
            try
            {
                RespuestaDTO<OfertaDTO> respuesta = await this._ofertaServicio.ModificarOferta(ofertaModificarDTO);
                if (respuesta == null)
                {
                    return NotFound("No se pudo modificar la oferta.");
                }
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
