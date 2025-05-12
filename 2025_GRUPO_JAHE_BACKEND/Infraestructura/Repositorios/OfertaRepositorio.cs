using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.Servicios;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Nucleo;
using Infraestructura.Nucleo;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infraestructura.Repositorios
{
    public class OfertaRepositorio : BaseRepositorio<Oferta>, IOfertaRepositorio
    {
        private readonly ContextoDbSQLServer _contexto;

        public OfertaRepositorio(ContextoDbSQLServer contexto) : base(contexto) 
        {
            _contexto = contexto;
        }

        public override async Task DeleteAsync(Oferta oferta)
        {
            oferta.Activa = false;

            await base.UpdateAsync(oferta);
        }

        public async Task<(IEnumerable<Oferta> ofertas, int datosTotales, int paginaActual)> VerOfertas(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina)
        {
            var ofertas = _contexto.Ofertas
                .Include(o => o.TipoDeHabitacion);

            int totalRegistros = await ofertas.CountAsync();

            int totalDePaginas = (int)Math.Ceiling((double)totalRegistros / maximoDeDatos);

            int paginaActual = irALaUltimaPagina ? (totalDePaginas == 0 ? 1 : totalDePaginas) : numeroDePagina;

            var resultados = await ofertas
                    .Skip((paginaActual - 1) * maximoDeDatos) 
                    .Take(maximoDeDatos) 
                    .ToListAsync(); 

            return (resultados, totalRegistros, paginaActual);
        }

        public async Task<IEnumerable<Oferta>> VerOfertasActivas()
        {
            var ofertas = await _contexto.Ofertas
                .Include(o => o.TipoDeHabitacion)
                .Include(o => o.Imagen)
                .Where(oferta => oferta.Activa == true)
                .ToListAsync();

            if (ofertas == null || !ofertas.Any())
                throw new Exception("No se encontraron ofertas activas.");

            return ofertas;
        }

        public async Task<Oferta> VerOfertaPorId(int idOferta)
        {
            var oferta = await _contexto.Ofertas
                .FirstOrDefaultAsync(o => o.IdOferta == idOferta);

            return oferta;
        }

    }
}
