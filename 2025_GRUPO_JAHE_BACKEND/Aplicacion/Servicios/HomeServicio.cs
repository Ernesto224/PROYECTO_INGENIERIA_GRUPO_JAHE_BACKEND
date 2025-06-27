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
            this._homeRepositorio = homeRepositorio;
            this._servicioAlmacenamientoImagenes = servicioAlmacenamientoImagenes;
        }

        public async Task<RespuestaDTO<HomeDTO>> ModificarDatosDeHome(HomeModificarDTO homeModificarDTO)
        {
            try
            {

                Home home = await this._homeRepositorio.VerDatosDeHome();

                home.Descripcion = homeModificarDTO.Descripcion;

                if (homeModificarDTO.Imagen != null)
                {
                    var urlImagen = await this._servicioAlmacenamientoImagenes
                        .SubirImagen(homeModificarDTO.Imagen, homeModificarDTO.NombreArchivo);
                    home.Imagen.Ruta = urlImagen;

                }

                await this._homeRepositorio.ModificarDatosDeHome(home);

                return new RespuestaDTO<HomeDTO>
                {
                    EsCorrecto = true,
                    Objeto = await this.VerDatosDeHome(),
                    Texto = "Datos del home actualizados correctamente"
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDTO<HomeDTO>()
                {
                    EsCorrecto = false,
                    Objeto = null,
                    Texto = ex.Message
                };
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
                    Url = home.Imagen.Ruta,
                }
            };
        }
    }
}
