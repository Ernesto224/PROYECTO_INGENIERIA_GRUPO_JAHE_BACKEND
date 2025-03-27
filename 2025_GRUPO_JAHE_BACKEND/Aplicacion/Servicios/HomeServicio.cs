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
    public class HomeServicio : IHomeServicio
    {
        private readonly IHomeRepositorio _homeRepositorio;
        private readonly IServicioAlmacenamientoImagenes _servicioAlmacenamientoImagenes;

        public HomeServicio(IHomeRepositorio homeRepositorio, IServicioAlmacenamientoImagenes servicioAlmacenamientoImagenes)
        {
            _homeRepositorio = homeRepositorio;
            _servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<object> ModificarDatosDeHome(HomeModificarDTO homeModificarDTO)
        {
            try
            {
                // Se sube la imagen al servicio de almacenamiento de imágenes
                var urlImagen = await this._servicioAlmacenamientoImagenes
                    .SubirImagen(homeModificarDTO.Imagen, homeModificarDTO.NombreArchivo);

                var home = new Home
                {
                    IdHome = homeModificarDTO.IdHome,
                    Descripcion = homeModificarDTO.Descripcion,
                    Imagen = new Imagen
                    {
                        Url = urlImagen,
                    }
                };

                return await this._homeRepositorio.ModificarDatosDeHome(home);
            }
            catch (Exception ex)
            {
                return ("Error", ex.Message);
            }
        }

        public async Task<HomeDTO> VerDatosDeHome()
        {
            var home = await this._homeRepositorio.VerDatosDeHome();

            if (home == null)
                throw new Exception("No se encontraron datos.");

            return new HomeDTO
            {
                IdHome = home.IdHome,
                Descripcion = home.Descripcion,
                Imagen = home.Imagen == null ? null : new ImagenDTO
                {
                    IdImagen = home.Imagen.IdImagen,
                    Url = home.Imagen.Url,
                }
            };
        }
    }
}
