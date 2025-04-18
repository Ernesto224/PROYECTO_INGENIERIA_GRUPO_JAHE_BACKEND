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
    public class FacilidadServicio : IFacilidadServicio
    {
        private readonly IFacilidadRepositorio _facilidadRepositorio;
        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public FacilidadServicio(IFacilidadRepositorio facilidadRepositorio, 
            IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            this._facilidadRepositorio = facilidadRepositorio;
            this._servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<object> ModificarInfromacionDeInstalacionYAtractivo(FacilidadModificarDTO facilidadModificarDTO)
        {
            try
            {
                // Si el atributo Imagen es nulo, no se sube una nueva imagen.
                string? urlImagen = null;

                if (facilidadModificarDTO.Imagen != null)
                {
                    // Se sube la imagen y obtenemos la URL
                    urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(facilidadModificarDTO.Imagen, facilidadModificarDTO.NombreArchivo);
                }

                // Se crea el objeto de la entidad a modificar
                var facilidad = new Facilidad
                {
                    IdFacilidad = facilidadModificarDTO.IdFacilidad,
                    Descripcion = facilidadModificarDTO.Descripcion,
                    Imagen = new Imagen
                    {
                        Url = urlImagen
                    }
                };

                // Aquí se pasa la lógica para que el repositorio actualice la habitación
                var facilidadActualizada = await this._facilidadRepositorio.ModificarInfromacionDeInstalacionYAtractivo(facilidad);
            
                return facilidadActualizada;  // Retornamos el objeto de respuesta
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar la información de la instalación o atractivo: {ex.Message}");
            }
        }

        public async Task<IEnumerable<FacilidadDTO>> VerInstalacionesYAtractivos()
        {
            var facilidades = await this._facilidadRepositorio.VerInstalacionesYAtractivos();

            if (facilidades == null)
                throw new Exception("No se encontraron datos.");

            return facilidades.Select(facilidad => new FacilidadDTO
                {
                IdFacilidad = facilidad.IdFacilidad,
                Descripcion = facilidad.Descripcion,
                Imagen = facilidad.Imagen == null ? null : new ImagenDTO
                {
                    IdImagen = facilidad.Imagen.IdImagen,
                    Url = facilidad.Imagen.Url,
                }
            }
            );
        }
    }
}
