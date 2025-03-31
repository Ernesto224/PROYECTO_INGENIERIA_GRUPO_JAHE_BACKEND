using Aplicacion.Interfaces;
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
    public class SobreNosotrosRepositorio: ISobreNosotrosRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public SobreNosotrosRepositorio(ContextoDbSQLServer contexto)
        {
            _contexto = contexto;
        }

        public async Task<SobreNosotros> VerDatosSobreNosotros()
        {
            return await _contexto.SobreNosotros
                .Include(sn => sn.ImagenesSobreNosotros)
                .ThenInclude(isn => isn.Imagen)
                .FirstOrDefaultAsync();
        }
    }
}
