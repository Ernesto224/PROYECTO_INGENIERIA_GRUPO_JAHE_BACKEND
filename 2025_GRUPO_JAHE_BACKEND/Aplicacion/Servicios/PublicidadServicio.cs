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

        public PublicidadServicio(IPublicidadRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<PublicidadDTO>> VerPublicidadesActivas()
        {
            var publicidades = await this._repositorio.VerPublicidadesActivas();

            if (publicidades == null)
                throw new Exception("No se encontraron datos.");

            return publicidades.Select(publicidad => new PublicidadDTO
            {
                IdPublicidad = publicidad.IdPublicidad,
                EnlacePublicidad = publicidad.EnlacePublicidad,
                Activo = publicidad.Activo,
                Imagen = new ImagenDTO
                {
                    IdImagen = publicidad.Imagen.IdImagen,
                    Url = publicidad.Imagen.Url
                }
            }).ToList();
        }
    }
}
