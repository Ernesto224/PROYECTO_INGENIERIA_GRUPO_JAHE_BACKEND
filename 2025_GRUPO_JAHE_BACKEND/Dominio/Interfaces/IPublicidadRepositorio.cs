using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Nucleo;

namespace Dominio.Interfaces
{
    public interface IPublicidadRepositorio : IRepositorio<Publicidad>
    {
        public Task<List<Publicidad>> VerPublicidadesActivas();
    }
}
