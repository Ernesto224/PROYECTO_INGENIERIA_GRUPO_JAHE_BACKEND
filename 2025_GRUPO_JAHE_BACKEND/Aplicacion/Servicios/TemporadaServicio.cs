using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Entidades;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class TemporadaServicio : ITemporadaServicio
    {
        private readonly ITemporadaRepositorio _temporadaRepositorio;

        private readonly ITransactionMethods _unitOfWork;

        public TemporadaServicio(ITemporadaRepositorio temporadaRepositorio, ITransactionMethods unitOfWork)
        {
            _temporadaRepositorio = temporadaRepositorio;
            _unitOfWork = unitOfWork;
        }


        public async Task<RespuestaDTO<TemporadaDTO>> ModificarTemporadaAlta(TemporadaDTO temporadaDTO)
        {
            try
            {

                var temporada = await this._temporadaRepositorio.ObtenerTemporadaAlta();

                temporada.Porcentaje = temporadaDTO.Porcentaje;
                temporada.FechaInicio = temporadaDTO.FechaInicio;
                temporada.FechaFinal = temporadaDTO.FechaFinal;

                await this._temporadaRepositorio.ModificarTemporada(temporada);

                var resultado = await this._unitOfWork.SaveChangesAsync();


                return new RespuestaDTO<TemporadaDTO>
                {
                    Texto = "Temporada modificada correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };

            }
            catch (Exception ex)
            {
                return new RespuestaDTO<TemporadaDTO>
                {
                    Texto = $"Error modificando la temporada: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
        }

        public async Task<TemporadaDTO> ObtenerTemporadaAlta()
        {
            try
            {
                var temporada = await this._temporadaRepositorio.ObtenerTemporadaAlta();
                if (temporada == null)
                {
                    return null;
                }
                return new TemporadaDTO
                {
                    IdTemporada = temporada.IdTemporada,
                    Nombre = temporada.Nombre,
                    FechaInicio = temporada.FechaInicio,
                    FechaFinal = temporada.FechaFinal,
                    Porcentaje = temporada.Porcentaje
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error obteniendo la temporada alta: {ex.Message}");
            }
        }
    }
}
