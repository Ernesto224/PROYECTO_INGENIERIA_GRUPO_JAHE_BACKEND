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

        public async Task<RespuestaDTO<PublicidadDTO>> CrearPublicidad(PublicidadCrearDTO publicidadCrearDTO)
        {
            try
            {
                // Si el atributo Imagen es nulo, no se sube una nueva imagen.
                string? urlImagen = null;

                if (publicidadCrearDTO.Imagen != null)
                {
                    // Se sube la imagen y obtenemos la URL
                    urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(publicidadCrearDTO.Imagen, publicidadCrearDTO.NombreArchivo);
                }
                else
                {
                    // Si no se proporciona una imagen, se lanza una excepción
                    throw new Exception("La imagen es obligatoria para crear una publicidad.");
                }

                // Se crea el objeto de la entidad a crear
                var publicidad = new Publicidad
                {
                    Enlace = publicidadCrearDTO.EnlacePublicidad,
                    Imagen = new Imagen
                    {
                        Ruta = urlImagen
                    }
                };

                // Se guarda la publicidad en el repositorio
                await this._repositorio.CrearAsync(publicidad);

                // Se guardan los cambios en la base de datos
                var resultado = await this._unitOfWork.SaveChangesAsync();

                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = "Publicidad creada correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };

            }
            catch (Exception ex)
            {
                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = $"Error creando publicidad: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
        }

        public async Task<PublicidadDTO> VerPublicidadPorId(int idPublicidad)
        {
            var publicidad = await this._repositorio.VerPubliciadadPorId(idPublicidad);
            if (publicidad == null)
                throw new Exception("No se encontró la publicidad.");
            return new PublicidadDTO
            {
                IdPublicidad = publicidad.IdPublicidad,
                EnlacePublicidad = publicidad.Enlace,
                Activo = publicidad.Activa,
                Imagen = new ImagenDTO
                {
                    IdImagen = publicidad.Imagen.IdImagen,
                    Url = publicidad.Imagen.Ruta
                }
            };
        }

        public async Task<RespuestaDTO<PublicidadDTO>> ModificarPublicidad(int idPublicidad, PublicidadCrearDTO publicidadModificarDTO)
        {
            try
            {
                var publicidad = await this._repositorio.VerPubliciadadPorId(idPublicidad);
                if (publicidad == null)
                    throw new Exception("No se encontró la publicidad a modificar.");
                // Actualizar los campos de la publicidad
                publicidad.Enlace = publicidadModificarDTO.EnlacePublicidad;
                // Si se proporciona una nueva imagen, actualizarla
                if (publicidadModificarDTO.Imagen != null)
                {
                    string urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(publicidadModificarDTO.Imagen, publicidadModificarDTO.NombreArchivo);
                    publicidad.Imagen.Ruta = urlImagen;
                }
                // Guardar los cambios en el repositorio
                await this._repositorio.UpdateAsync(publicidad);
                var resultado = await this._unitOfWork.SaveChangesAsync();
                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = "Publicidad modificada correctamente",
                    EsCorrecto = true,
                    Objeto = null
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<PublicidadDTO>
                {
                    Texto = $"Error modificando la publicidad: {ex.Message}",
                    EsCorrecto = false,
                    Objeto = null
                };
            }
        }
    }
}