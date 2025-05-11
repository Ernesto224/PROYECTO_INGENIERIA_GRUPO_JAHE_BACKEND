using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class PublicidadRepositorio : IPublicidadRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public PublicidadRepositorio(ContextoDbSQLServer contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Publicidad>> VerPublicidadesActivas()
        {
            try
            {
                var publicidades = await this._contexto.Publicidades
                    .Include(publicidades => publicidades.Imagen)
                    .Where(publicidad => publicidad.Imagen!.Eliminado == false)
                    .Where(publicidad => publicidad.Activa == true)
                    .ToListAsync();

                if (publicidades == null)
                    throw new Exception("No se encontraron datos.");

                return publicidades;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
