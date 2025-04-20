using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura.Repositorios
{
    public class AdministradorRepositorio : IAdministradorRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public AdministradorRepositorio (ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<(bool loginCorrecto, Administrador administradorRecuperado)> LoginAdministrador(Administrador administrador)
        {
            try 
            { 
                var administradorDb = await this._contexto.Administradores
                    .FirstOrDefaultAsync<Administrador>(administradorDb => (
                        administradorDb.NombreDeUsuario == administrador.NombreDeUsuario
                    ) && (
                        administradorDb.Contrasennia == administrador.Contrasennia
                    ));

                if (administradorDb == null) 
                {
                    return (false, administradorDb!);
                }

                return (true, administradorDb);
            } 
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Administrador> ObtenerInformacionDelAdministrador(int idAdmin)
        {
            try
            {
                var administradorDb = await this._contexto.Administradores
                   .FirstOrDefaultAsync<Administrador>(administradorDb => administradorDb.IdAdmin == idAdmin);

                if (administradorDb == null)
                {
                    return null!;
                }

                return administradorDb;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
