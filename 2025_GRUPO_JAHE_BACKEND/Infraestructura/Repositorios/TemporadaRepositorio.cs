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
    public class TemporadaRepositorio : ITemporadaRepositorio
    {
        private readonly ContextoDbSQLServer _context;

        public TemporadaRepositorio(ContextoDbSQLServer context)
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

    }
}
