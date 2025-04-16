using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController : ControllerBase
    {

        private readonly ITarifasServicio _tarifasServicio;

        public TarifasController(ITarifasServicio tarifasServicio)
        {
            this._tarifasServicio = tarifasServicio;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDeHabitacionDTO>>> Get()
        {
            try
            {
                var tipoDeHabitacionesDTO = await this._tarifasServicio.verTarifas();

                if (tipoDeHabitacionesDTO == null)
                    return NotFound("No se encontraron datos.");

                return Ok(tipoDeHabitacionesDTO);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<object>> Put(TipoDeHabitacionModificarDTO tipoDeHabitacionModificarDTO)
        {
            try
            {
                var resultado = await this._tarifasServicio.ActualizarTipoDeHabitacion(tipoDeHabitacionModificarDTO);

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



    }


}
