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
        public async Task<SobreNosotrosDTO> VerDatosSobreNosotros()
        {
            var sobreNosotros = await _sobreNosotrosRepositorio.VerDatosSobreNosotros();

            if (sobreNosotros == null)
                throw new Exception("No se encontraron datos.");

            return new SobreNosotrosDTO
            {
                Descripcion = sobreNosotros.Descripcion,
                Imagenes = sobreNosotros.ImagenesSobreNosotros
                    .Where(i => !i.Imagen.Eliminado)
                    .Select(i => new ImagenDTO
                    {
                        IdImagen = i.Imagen.IdImagen,
                        Url = i.Imagen.Url
                    }).ToList()
            };
        }
    }
}
