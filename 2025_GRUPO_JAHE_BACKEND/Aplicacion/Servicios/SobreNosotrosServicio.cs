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

        public SobreNosotrosServicio(ISobreNosotrosRepositorio sobreNosotrosRepositorio)
        {
            _sobreNosotrosRepositorio = sobreNosotrosRepositorio;
        }

        public async Task<SobreNosotrosDTO> CambiarImagenGaleriaSobreNosotros(SobreNosotrosDTO sobreNosotrosDTO)
        {
            var sobreNosotroActualizado = await _sobreNosotrosRepositorio.CambiarImagenGaleriaSobreNosotros(new SobreNosotros
            {
                ImagenesSobreNosotros = sobreNosotrosDTO.Imagenes.Select(imagen => new Imagen_SobreNosotros
                {
                    IdImagen = imagen.IdImagen,
                }).ToList()
            });
            return new SobreNosotrosDTO
            {
                Descripcion = sobreNosotroActualizado.Descripcion,
                Imagenes = sobreNosotroActualizado.ImagenesSobreNosotros.Where(imagen => !imagen.Imagen.Eliminado)
                .Select(img=> new ImagenDTO
                {
                    IdImagen = img.Imagen.IdImagen,
                    Url = img.Imagen.Url
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
                    .Where(imagen => !imagen.Imagen.Eliminado)
                    .Select(img => new ImagenDTO
                    {
                        IdImagen = img.Imagen.IdImagen,
                        Url = img.Imagen.Url
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
                Descripcion = sobreNosotros.Descripcion,
                Imagenes = sobreNosotros.ImagenesSobreNosotros
                    .Where(imagen => !imagen.Imagen.Eliminado)
                    .Select(img => new ImagenDTO
                    {
                        IdImagen = img.Imagen.IdImagen,
                        Url = img.Imagen.Url
                    }).ToList()
            };
        }
    }
}
