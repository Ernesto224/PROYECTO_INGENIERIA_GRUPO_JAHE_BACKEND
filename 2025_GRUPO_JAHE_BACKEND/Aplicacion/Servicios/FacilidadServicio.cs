using Aplicacion.DTOs;
using Aplicacion.Interfaces;
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

        public FacilidadServicio(IFacilidadRepositorio facilidadRepositorio)
        {
            this._facilidadRepositorio = facilidadRepositorio;
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
