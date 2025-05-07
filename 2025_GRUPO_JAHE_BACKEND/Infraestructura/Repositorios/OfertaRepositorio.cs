using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Nucleo;
using Infraestructura.Nucleo;
using Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace Infraestructura.Repositorios
{
    public class OfertaRepositorio : Repository<Oferta>, IOfertaRepositorio
    {

        public OfertaRepositorio(ContextoDbSQLServer contexto) : base(contexto) { }

        //public async Task<Oferta> VerOfertaPorFecha(int idTipoHabitacion, DateTime fechaInicio, DateTime fechaFinal)
        //{
        //    var oferta = await this._contexto.Ofertas
        //        .Where(oferta => oferta.IdTipoDeHabitacion == idTipoHabitacion)
        //        .FirstOrDefaultAsync(o => o.FechaInicio == fechaInicio && o.FechaFinal == fechaFinal);
        //    if (oferta == null)
        //        return null;

        //    return oferta;
        //}

        //public async Task<List<Oferta>> VerOfertasActivas()
        //{
        //    try
        //    {
        //        var ofertas = await this._contexto.Ofertas
        //            .Include(o => o.TipoDeHabitacion)
        //            .Include(o => o.TipoDeHabitacion.Imagen)
        //            .Where(oferta => oferta.Activo == true)
        //            .ToListAsync();

        //        if (ofertas == null || !ofertas.Any())
        //            throw new Exception("No se encontraron ofertas activas.");

        //        return ofertas;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}




    }
}
