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
    public class FacilidadRepositorio : IFacilidadRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public FacilidadRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<IEnumerable<Facilidad>> VerInstalacionesYAtractivos()
        {
            try
            {
                var facilidades = await this._contexto.Facilidades
                    .Include(facilidad => facilidad.Imagen)
                    .Where(facilidad => facilidad.Imagen!.Eliminado == false)
                    .ToListAsync<Facilidad>();

                if (facilidades == null)
                    throw new Exception("No se encontraron datos.");

                return facilidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
