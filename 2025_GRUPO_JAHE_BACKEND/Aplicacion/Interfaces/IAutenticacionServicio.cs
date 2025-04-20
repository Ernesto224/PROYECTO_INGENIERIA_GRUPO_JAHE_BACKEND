using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IAutenticacionServicio
    {
        public Task<RespuestaAutenticacionDTO> LoginAdministrador(AdministradorLoginDTO administradorLoginDTO);
        public Task<TokenDeRespuestaDTO> RefrescarTokensDeAcceso(string token);
    }
}
