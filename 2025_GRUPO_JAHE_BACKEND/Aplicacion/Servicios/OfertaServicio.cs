using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;

namespace Aplicacion.Servicios
{
    public class OfertaServicio : IOfertaServicio
    {
        private readonly IOfertaRepositorio _ofertaRepositorio;

        private readonly ITransactionMethods _unitOfWork;

        public OfertaServicio(IOfertaRepositorio ofertaRepositorio, ITransactionMethods unitOfWork)
        {
            _ofertaRepositorio = ofertaRepositorio;
            _unitOfWork = unitOfWork;
        }

        public async Task<OfertaDTO> CrearOferta(OfertaDTO ofertaDTO)
        {
            var oferta = new Oferta()
            {
                IdOferta = ofertaDTO.IdOferta,
                Nombre = ofertaDTO.Nombre,
                FechaInicio = ofertaDTO.FechaInicio,
                FechaFinal = ofertaDTO.FechaFinal,
                Porcentaje = ofertaDTO.Porcentaje,
                Activa = ofertaDTO.Activo,
                IdTipoDeHabitacion = ofertaDTO.TipoDeHabitacion.IdTipoDeHabitacion
            };

            await this._ofertaRepositorio.CrearAsync(oferta);

            await this._unitOfWork.SaveChangesAsync();

            return ofertaDTO;
        }

        public async Task<List<OfertaDTO>> VerOfertas()
        {
            var ofertas = await _ofertaRepositorio.GetAllAsync<Oferta>(o => o.TipoDeHabitacion);

            return ofertas.Select(o => new OfertaDTO
            {
                IdOferta = o.IdOferta,
                Nombre = o.Nombre,
                FechaInicio = o.FechaInicio,
                FechaFinal = o.FechaFinal,
                Porcentaje = o.Porcentaje,
                Activo = o.Activa,
                TipoDeHabitacion = new TipoDeHabitacionDTO 
                {
                    IdTipoDeHabitacion = o.IdTipoDeHabitacion,
                    Nombre = o.TipoDeHabitacion.Nombre,
                    TarifaDiaria = (decimal)o.TipoDeHabitacion.TarifaDiaria
                }
            }).ToList();
        }

        //public async Task<List<OfertaDTO>> VerOfertasActivas()
        //{
        //    var ofertas = await this._ofertaRepositorio.VerOfertasActivas();

        //    if (ofertas == null || !ofertas.Any())
        //        throw new Exception("No se encontraron ofertas activas.");

        //    return ofertas.Select(oferta => new OfertaDTO
        //    {
        //        IdOferta = oferta.IdOferta,
        //        FechaInicio = oferta.FechaInicio,
        //        FechaFinal = oferta.FechaFinal,
        //        Nombre = oferta.Nombre,
        //        Porcentaje = oferta.Porcentaje,
        //        Activo = oferta.Activo,
        //        TipoDeHabitacion = new TipoDeHabitacionDTO
        //        {
        //            IdTipoDeHabitacion = oferta.IdTipoDeHabitacion,
        //            Nombre = oferta.TipoDeHabitacion.Nombre,
        //            Descripcion = oferta.TipoDeHabitacion.Descripcion,
        //            TarifaDiaria = (decimal)oferta.TipoDeHabitacion.TarifaDiaria,
        //            Imagen = new ImagenDTO
        //            {
        //                IdImagen = oferta.TipoDeHabitacion.Imagen.IdImagen,
        //                Ruta = oferta.TipoDeHabitacion.Imagen.Ruta
        //            }
        //        }
        //    }).ToList();
        //}


    }
}
