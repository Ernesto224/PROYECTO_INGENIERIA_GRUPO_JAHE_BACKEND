using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly IContactoServicio _contactoServicio;

        public ContactoController(IContactoServicio contactoServicio)
        {
            this._contactoServicio = contactoServicio;
        }
        [HttpGet]
        public async Task<ActionResult<ContactoDTO>> Get()
        {
            try
            {
                var contactoDTO = await _contactoServicio.VerDatosContacto();
                return Ok(contactoDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error en al base de datos");
            }
        }
    }
}
