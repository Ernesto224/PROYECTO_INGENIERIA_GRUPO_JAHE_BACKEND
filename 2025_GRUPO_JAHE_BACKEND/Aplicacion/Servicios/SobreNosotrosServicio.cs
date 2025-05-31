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
    public class SobreNosotrosServicio: ISobreNosotrosServicio
    {
        private readonly ISobreNosotrosRepositorio _sobreNosotrosRepositorio;
        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public SobreNosotrosServicio(ISobreNosotrosRepositorio sobreNosotrosRepositorio, IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            _sobreNosotrosRepositorio = sobreNosotrosRepositorio;
            _servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<SobreNosotrosDTO> CambiarImagenGaleriaSobreNosotros(SobreNosotrosModificarDTO galeriaModificarDTO)
        {
            Console.WriteLine("DATOS EN EL SERVICIO: " + galeriaModificarDTO.IdSobreNosotros + " ID_Imagen " + galeriaModificarDTO.IdImagen + " Nombre Archivo: " + galeriaModificarDTO.Imagen + " " + galeriaModificarDTO.NombreArchivo);
            // Si el atributo Imagen es nulo, no se sube una nueva imagen.  
            string? urlImagen = null;

            if (galeriaModificarDTO.Imagen != null)
            {
                // Se sube la imagen y obtenemos la URL  
                urlImagen = await this._servicioAlmacenamientoImagenes
                    .SubirImagen(galeriaModificarDTO.Imagen, galeriaModificarDTO.NombreArchivo);
            }
            Console.WriteLine("NUEVA URL EN EL SERVICIO: " +urlImagen);

            var sobreNosotroActualizado = await _sobreNosotrosRepositorio.CambiarImagenGaleriaSobreNosotros(new SobreNosotros
            {
                IdSobreNosotros = galeriaModificarDTO.IdSobreNosotros,
                ImagenesSobreNosotros = new List<Imagen_SobreNosotros>
               {
                   new Imagen_SobreNosotros
                   {
                       IdImagen = galeriaModificarDTO.IdImagen,
                       IdSobreNosotros = galeriaModificarDTO.IdSobreNosotros,
                   }
               }
            }, urlImagen);

            return new SobreNosotrosDTO
            {
                Descripcion = sobreNosotroActualizado.Descripcion,
                Imagenes = sobreNosotroActualizado.ImagenesSobreNosotros.
                Where(imagen => imagen.Imagen != null && imagen.Imagen.Activa)
                .Select(img => new ImagenDTO
                {
                    IdImagen = img.Imagen.IdImagen,
                    Url = img.Imagen.Ruta
                }).ToList()
            };
        }

        public async Task<SobreNosotrosDTO> CambiarTextoSobreNosotros(SobreNosotrosDTO sobreNosotrosDTO)
        {
            var sobreNosotrosActualizado = await _sobreNosotrosRepositorio.CambiarTextoSobreNosotros(new SobreNosotros {
                Descripcion = sobreNosotrosDTO.Descripcion,
            });

            return new SobreNosotrosDTO
            {
                Descripcion = sobreNosotrosActualizado.Descripcion,
                Imagenes = sobreNosotrosActualizado.ImagenesSobreNosotros
                    .Where(imagen => imagen.Imagen != null && imagen.Imagen.Activa)
                    .Select(img => new ImagenDTO
                    {
                        IdImagen = img.Imagen.IdImagen,
                        Url = img.Imagen.Ruta
                    }).ToList()

            };
        }

        public async Task<SobreNosotrosDTO> VerDatosSobreNosotros()
        {
            var sobreNosotros = await _sobreNosotrosRepositorio.VerDatosSobreNosotros();

            if (sobreNosotros == null)
                throw new Exception("No se encontraron datos.");

            return new SobreNosotrosDTO
            {
                idSobreNosotros = sobreNosotros.IdSobreNosotros,
                Descripcion = sobreNosotros.Descripcion,
                Imagenes = sobreNosotros.ImagenesSobreNosotros
                    .Where(imagen => imagen.Imagen != null && imagen.Imagen.Activa)
                    .Select(img => new ImagenDTO
                    {
                        IdImagen = img.Imagen.IdImagen, 
                        Url = img.Imagen.Ruta
                    }).ToList()
            };
        }
    }
}
