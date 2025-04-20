using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces.Seguridad
{
    public interface IServicioGeneracionDeTokens
    {
        public Task<(string tokenDeAcceso, string tokenDeRefresco, DateTime fechaDeExpiracion)> GenerarTokenDeAcceso(Administrador administrador);
        public Task<string> ValidarToken(string token);
    }
}
