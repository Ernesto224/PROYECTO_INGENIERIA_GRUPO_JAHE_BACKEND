using Dominio.Entidades;
using Dominio.Interfaces.Seguridad;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Seguridad
{
    public class ServicioGeneracionDeTokens : IServicioGeneracionDeTokens
    {
        private readonly IConfiguration _configuration;

        public ServicioGeneracionDeTokens (IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public Task<(string tokenDeAcceso, string tokenDeRefresco, DateTime fechaDeExpiracion)> GenerarTokenDeAcceso(Usuario administrador)
        {
            try 
            {
                // Definimos los "claims" que irán dentro del token de acceso (Access Token)
                // Estos representan la identidad y atributos del usuario
                var claimsUsuario = new[]
                {
                new Claim(ClaimTypes.NameIdentifier, administrador.IdUsuario.ToString()), // ID del usuario como identificador único
                new Claim(ClaimTypes.Email, administrador.NombreUsuario), // Correo del usuario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único del token (para trazabilidad)
            };

                // Se construye la clave secreta a partir de la configuración de la app (appsettings.json)
                var claveSegura = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey:key"]!));

                // Se crean las credenciales de firma usando el algoritmo HMAC-SHA256
                var credenciales = new SigningCredentials(claveSegura, SecurityAlgorithms.HmacSha256Signature);

                // Definimos cuándo expira el token de acceso (15 minutos desde ahora)
                var expiracion = DateTime.UtcNow.AddMinutes(15);

                // Creamos el token de acceso (JWT) que llevará los claims definidos
                var tokenDeAcceso = new JwtSecurityToken(
                    claims: claimsUsuario, // Incluye la identidad del usuario
                    expires: expiracion, // Tiempo de expiración del token
                    signingCredentials: credenciales // Firma digital para garantizar autenticidad
                );

                var tokenDeRefresco = this.GenerarTokenDeRefresco(administrador, credenciales);

                // Se retorna un objeto anónimo con los tokens serializados y la fecha de expiración del token de acceso
                return Task.FromResult((
                    new JwtSecurityTokenHandler().WriteToken(tokenDeAcceso), // Token corto para autenticación
                    tokenDeRefresco, // Token para renovar acceso
                    expiracion // Fecha en que vence el token de acceso
                ));
            } 
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message);
            }
        }

        private string GenerarTokenDeRefresco(Usuario administrador, SigningCredentials credenciales)
        {
            // Creamos los claims para el token de refresco (Refresh Token)
            // Estos deben ser mínimos, y pueden incluir un flag que lo distinga como tal
            var claimsRefresco = new[]
            {
                new Claim("refresh", "true"), // Flag que indica que este token es de tipo "refresh"
                new Claim(ClaimTypes.NameIdentifier, administrador.IdUsuario.ToString()) // ID del usuario
            };

            // Se genera el token de refresco con una duración más larga (1 hora)
            var tokenDeRefresco = new JwtSecurityToken(
                claims: claimsRefresco,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDeRefresco);
        }

        public Task<string> ValidarToken(string token)
        {
            try 
            {
                var handler = new JwtSecurityTokenHandler();// Manjador que gestiona la creacion y valides de tokens
                var tokenValidado = handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtSecretKey:key"]!))
                }, out SecurityToken tokenSeguro);// Parametros del token que deben ser iguales a los de la creacion del token

                var claimsPrincipal = (ClaimsPrincipal)tokenValidado;
                return Task.FromResult(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)!.Value);//devulve el id oculto en el token cifrado
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
