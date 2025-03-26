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

        public async Task<Home> VerDatosDeHome()
        {
            try 
            { 
                var home = await this._contexto.Homes.Include(home => home.Imagen).FirstOrDefaultAsync<Home>();
                
                return home;
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message); 
            }
        }
    }
}
