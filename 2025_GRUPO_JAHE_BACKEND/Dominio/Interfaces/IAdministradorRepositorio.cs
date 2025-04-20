using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IAdministradorRepositorio
    {
        public Task<(bool loginCorrecto, Administrador administradorRecuperado)> LoginAdministrador(Administrador administrador);

        public Task<Administrador> ObtenerInformacionDelAdministrador(int idAdmin);
    }
}
