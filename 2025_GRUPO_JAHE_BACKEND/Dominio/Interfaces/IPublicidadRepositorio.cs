using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Nucleo;

namespace Dominio.Interfaces
{
    public interface IPublicidadRepositorio : IBaseRepositorio<Publicidad>
    {
        public Task<List<Publicidad>> VerPublicidadesActivas();

        public Task<Publicidad> VerPubliciadadPorId(int idPublicidad);

       
    }
}
