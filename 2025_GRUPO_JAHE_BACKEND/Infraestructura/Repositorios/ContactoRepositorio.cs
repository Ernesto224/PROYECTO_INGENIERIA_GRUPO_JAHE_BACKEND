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
    public class ContactoRepositorio : IContactoRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public ContactoRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }
        public async Task<Contacto> VerDatosContacto()
        {
            return await _contexto.Contactos.FirstOrDefaultAsync();
        }
    }
}
