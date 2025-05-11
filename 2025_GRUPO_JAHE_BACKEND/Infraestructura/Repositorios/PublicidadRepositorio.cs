using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Infraestructura.Nucleo;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class PublicidadRepositorio : BaseRepositorio<Publicidad>, IPublicidadRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public PublicidadRepositorio(ContextoDbSQLServer contexto) : base(contexto)
        {
            _contexto = contexto;
        }

        public new async Task CrearAsync<Publicidad>(Publicidad entity) where Publicidad : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _contexto.Set<T>().AddAsync(entity);
        }

        public async Task<List<Publicidad>> VerPublicidadesActivas()
        {
            try
            {
                var publicidades = await this._contexto.Publicidades
                    .Include(publicidades => publicidades.Imagen)
                    .Where(publicidad => publicidad.Imagen!.Activa == false)
                    .Where(publicidad => publicidad.Activo == true)
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
