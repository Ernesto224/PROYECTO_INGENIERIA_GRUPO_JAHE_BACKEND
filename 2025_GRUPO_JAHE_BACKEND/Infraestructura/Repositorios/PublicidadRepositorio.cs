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

        public override async Task DeleteAsync(Publicidad publicidad)
        {
            publicidad.Activa = !publicidad.Activa;

            await base.UpdateAsync(publicidad);
        }

        public async Task<List<Publicidad>> VerPublicidadesActivas()
        {
            try
            {
                var publicidades = await this._contexto.Publicidades
                    .Include(publicidades => publicidades.Imagen)
                    .Where(publicidad => publicidad.Imagen!.Activa == true)
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


        public async Task<Publicidad> VerPubliciadadPorId(int idPublicidad)
        {
            var publiciadad = await _contexto.Publicidades
                .FirstOrDefaultAsync(o => o.IdPublicidad == idPublicidad);

            return publiciadad;
        }


        public override async Task CrearAsync(Publicidad publicidad)
        {
            if (publicidad == null)
                throw new ArgumentNullException(nameof(publicidad));

            await _contexto.Publicidades.AddAsync(publicidad);
            await _contexto.SaveChangesAsync();
        }

    }
}
