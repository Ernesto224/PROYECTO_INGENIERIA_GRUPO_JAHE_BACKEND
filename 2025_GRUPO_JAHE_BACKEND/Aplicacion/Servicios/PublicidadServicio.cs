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
    public class PublicidadServicio : IPublicidadServicio
    {
        private readonly IPublicidadRepositorio _repositorio;

        private readonly ITransactionMethods _unitOfWork;

        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public PublicidadServicio(IPublicidadRepositorio repositorio, ITransactionMethods unitOfWork, IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            _repositorio = repositorio;
            _unitOfWork = unitOfWork;
            _servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<List<PublicidadDTO>> VerPublicidadesActivas()
        {
            var publicidades = await this._repositorio.VerPublicidadesActivas();

            if (publicidades == null)
                throw new Exception("No se encontraron datos.");

            return publicidades.Select(publicidad => new PublicidadDTO
            {
                IdPublicidad = publicidad.IdPublicidad,
                EnlacePublicidad = publicidad.Enlace,
                Activo = publicidad.Activa,
                Imagen = new ImagenDTO
                {
                    IdImagen = publicidad.Imagen.IdImagen,
                    Url = publicidad.Imagen.Ruta
                }
            }).ToList();
        }


        public async Task<RespuestaDTO<PublicidadDTO>> EliminarPublicidad(int idOferta)
        {
            try
            {
                var oferta = await this._repositorio.VerPubliciadadPorId(idOferta);

                await this._repositorio.DeleteAsync(oferta);

                var resultado = await this._unitOfWork.SaveChangesAsync();


                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = "Eliminada correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };

            }
            catch (Exception ex)
            {
                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = $"Error eliminando oferta: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }

        }



    }
}
