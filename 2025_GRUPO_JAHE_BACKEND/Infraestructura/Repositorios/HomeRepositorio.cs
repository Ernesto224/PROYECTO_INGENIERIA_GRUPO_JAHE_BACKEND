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
    public class HomeRepositorio : IHomeRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public HomeRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<bool> ModificarDatosDeHome(Home home)
        {
            try
            {

                var homeDb = await this.VerDatosDeHome();

                if (homeDb == null)
                        throw new Exception("No se encontraron datos.");

                homeDb.Descripcion = home.Descripcion;
                homeDb.Imagen!.Ruta = home.Imagen!.Ruta;

                return await _contexto.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Home> VerDatosDeHome()
        {
            try 
            { 
                var home = await this._contexto.Homes
                    .Include(home => home.Imagen)
                    .Where(home => home.Imagen!.Activa == true)
                    .FirstOrDefaultAsync<Home>();

                if (home == null)
                    throw new Exception("No se encontraron datos.");

                return home;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
