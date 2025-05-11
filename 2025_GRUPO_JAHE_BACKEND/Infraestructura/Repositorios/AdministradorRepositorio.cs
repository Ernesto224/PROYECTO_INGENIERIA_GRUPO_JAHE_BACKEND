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

        public async Task<(bool loginCorrecto, Usuario administradorRecuperado)> LoginAdministrador(Usuario administrador)
        {
            try 
            { 
                var administradorDb = await this._contexto.Administradores
                    .FirstOrDefaultAsync<Usuario>(administradorDb => (
                        administradorDb.NombreUsuario == administrador.NombreUsuario
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

        public async Task<Usuario> ObtenerInformacionDelAdministrador(int idAdmin)
        {
            try
            {
                var administradorDb = await this._contexto.Administradores
                   .FirstOrDefaultAsync<Usuario>(administradorDb => administradorDb.IdUsuario == idAdmin);

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
