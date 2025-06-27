using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2025_GRUPO_JAHE_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly IAutenticacionServicio _autenticacionServicio;

        public AutenticacionController (IAutenticacionServicio autenticacionServicio)
        {
            _autenticacionServicio = autenticacionServicio;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<RespuestaAutenticacionDTO>> LoginAdministrador(AdministradorLoginDTO administradorLoginDTO) 
        {
            try 
            { 
                var resultadoAutenticacion = await this._autenticacionServicio.LoginAdministrador(administradorLoginDTO);

                return Ok(resultadoAutenticacion);
            } 
            catch (Exception ex) 
            {
                return NotFound(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult<TokenDeRespuestaDTO>> RefrescarTokensDeAcceso([FromHeader] string authorization)
        {
            try
            {
                var tokenDeRefresco = authorization.Replace("Bearer ", "").Trim();

                // Se optiene desde el header de la solicitud el token de refresco y se quita el "Bearer " adjunto
                var resultadoVerificacion = await this._autenticacionServicio.RefrescarTokensDeAcceso(tokenDeRefresco);

                if (resultadoVerificacion == null)
                {
                    return Unauthorized();
                }

                return Ok(resultadoVerificacion);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
