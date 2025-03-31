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
    public class OfertaRepositorio: IOfertaRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public OfertaRepositorio(ContextoDbSQLServer contexto)
        {
            this._contexto = contexto;
        }

        public async Task<List<Oferta>> VerOfertasActivas()
        {
            try
            {
                var ofertas = await this._contexto.Ofertas
                    .Include(o => o.TipoDeHabitacion)
                    .Include(o => o.TipoDeHabitacion.Imagen)
                    .Where(oferta => oferta.Activo == true)
                    .ToListAsync();

                if (ofertas == null || !ofertas.Any())
                    throw new Exception("No se encontraron ofertas activas.");

                return ofertas;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
