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
    public class TemporadaRepositorio : BaseRepositorio<Temporada>, ITemporadaRepositorio
    {
        private readonly ContextoDbSQLServer _context;

        public TemporadaRepositorio(ContextoDbSQLServer context) : base(context)
        {
            _context = context;
        }

        public async Task<Temporada> ObtenerTemporadaPorFecha(DateTime fechaInicio, DateTime fechaFinal)
        {
            var temporada = await _context.Temporadas
                .FirstOrDefaultAsync(t => t.FechaInicio <= fechaInicio && t.FechaFinal >= fechaFinal);

            if(temporada == null)
            {
                return null;
            }

            return temporada;
        }

        public async Task<Temporada> ObtenerTemporadaAlta()
        {
            var temporada = await _context.Temporadas
                .FirstOrDefaultAsync(t => t.Nombre == "Alta Temporada");

            if (temporada == null)
            {
                return null;
            }

            return temporada;
        }

        public async Task<Temporada> ModificarTemporada(Temporada temporada)
        {
            if (temporada == null) throw new ArgumentNullException(nameof(temporada));
            _context.Temporadas.Update(temporada);
            await _context.SaveChangesAsync();
            return temporada;
        }




        }
}
