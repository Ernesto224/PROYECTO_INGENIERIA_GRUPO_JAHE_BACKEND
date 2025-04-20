using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Interfaces.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class AutenticacionServicio : IAutenticacionServicio
    {
        private readonly IAdministradorRepositorio _administradorRepositorio;
        private readonly IServicioGeneracionDeTokens _servicioGeneracionDeTokens;

        public AutenticacionServicio (IAdministradorRepositorio administradorRepositorio, 
            IServicioGeneracionDeTokens servicioGeneracionDeTokens)
        {
            this._administradorRepositorio = administradorRepositorio;
            this._servicioGeneracionDeTokens = servicioGeneracionDeTokens;
        }

        public async Task<RespuestaAutenticacionDTO> LoginAdministrador(AdministradorLoginDTO administradorLoginDTO)
        {
            var administrador = new Administrador 
            {
                NombreDeUsuario = administradorLoginDTO.NombreDeUsuario,
                Contrasennia = administradorLoginDTO.Contrasennia,
            };

            // Se verifican las credenciales de un administrador
            var resultado = await this._administradorRepositorio.LoginAdministrador(administrador);

            if (!resultado.loginCorrecto)
            {
                return new RespuestaAutenticacionDTO 
                {
                    Icon = "error",
                    Text = "Usuario no encontrado."
                };
            }

            // Generacion de un token
            var tokenGenerado = this._servicioGeneracionDeTokens.GenerarTokenDeAcceso(resultado.administradorRecuperado);

            return new RespuestaAutenticacionDTO 
            { 
                Icon = "success", 
                Text = "Usuario Verificado Correctamente",
                TokenDeRespuesta = new TokenDeRespuestaDTO 
                {
                    TokenDeAcceso = tokenGenerado.Result.tokenDeAcceso,
                    TokenDeRefresco = tokenGenerado.Result.tokenDeRefresco,
                    ExpiraEn = tokenGenerado.Result.fechaDeExpiracion
                }
            };

        }

        public async Task<TokenDeRespuestaDTO> RefrescarTokensDeAcceso(string token)
        {
            // Se verifica primero la validez del token de refresco
            var idAdmin = int.Parse(this._servicioGeneracionDeTokens.ValidarToken(token).Result);

            var resultado = await this._administradorRepositorio.ObtenerInformacionDelAdministrador(idAdmin);

            if (resultado == null) 
            {
                return null!;
            }

            // Generacion de un token
            var tokenGenerado = this._servicioGeneracionDeTokens.GenerarTokenDeAcceso(resultado);

            return new TokenDeRespuestaDTO
            {
                TokenDeAcceso = tokenGenerado.Result.tokenDeAcceso,
                TokenDeRefresco = tokenGenerado.Result.tokenDeRefresco,
                ExpiraEn = tokenGenerado.Result.fechaDeExpiracion
            };
        }
    }
}
