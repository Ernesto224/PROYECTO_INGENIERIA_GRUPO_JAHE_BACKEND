using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class OfertaServicio : IOfertaServicio
    {
        private readonly IOfertaRepositorio _ofertaRepositorio;

        public OfertaServicio(IOfertaRepositorio ofertaRepositorio)
        {
            _ofertaRepositorio = ofertaRepositorio;
        }

        public async Task<List<OfertaDTO>> VerOfertasActivas()
        {
            var ofertas = await this._ofertaRepositorio.VerOfertasActivas();

            if (ofertas == null || !ofertas.Any())
                throw new Exception("No se encontraron ofertas activas.");

            return ofertas.Select(oferta => new OfertaDTO
            {
                IdOferta = oferta.IdOferta,
                FechaInicio = oferta.FechaInicio,
                FechaFinal = oferta.FechaFinal,
                Nombre = oferta.Nombre,
                Porcentaje = oferta.Porcentaje,
                Activo = oferta.Activo,
                TipoDeHabitacion = new TipoDeHabitacionDTO
                {
                    IdTipoDeHabitacion = oferta.IdTipoDeHabitacion,
                    Nombre = oferta.TipoDeHabitacion.Nombre,
                    Descripcion = oferta.TipoDeHabitacion.Descripcion,
                    TarifaDiaria = (decimal)oferta.TipoDeHabitacion.TarifaDiaria,
                    Imagen = new ImagenDTO
                    {
                        IdImagen = oferta.TipoDeHabitacion.Imagen.IdImagen,
                        Url = oferta.TipoDeHabitacion.Imagen.Url
                    }
                }
            }).ToList();
        }
    }
}
