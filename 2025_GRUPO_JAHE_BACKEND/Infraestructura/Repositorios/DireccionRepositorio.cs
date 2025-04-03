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

    public class DireccionRepositorio : IDireccionRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;
        public DireccionRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }
        public async Task<Direccion> VerDatosDireccion()
        {
            var direccion = await this._contexto.Direccion.FirstOrDefaultAsync();
            return direccion;
        }
    }
}
