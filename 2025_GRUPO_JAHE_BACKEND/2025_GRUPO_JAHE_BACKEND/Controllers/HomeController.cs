using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHomeServicio _homeServicio;

        public HomeController(IHomeServicio homeServicio)
        {
            this._homeServicio = homeServicio;
        }

        // GET api/<HomeController>/5
        [HttpGet]
        public async Task<ActionResult<HomeDTO>> Get()
        {
            try
            {
                var HomeDTO = await this._homeServicio.VerDatosDeHome();

                if (HomeDTO == null)
                    return NotFound("No se encontraron datos.");

                return Ok(HomeDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // PUT api/<HomeController>/5
        [HttpPut]
        public async Task<ActionResult<object>> Put(HomeModificarDTO homeModificarDTO)
        {
            try
            {
                var resultado = await this._homeServicio.ModificarDatosDeHome(homeModificarDTO);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
      
    }
}
