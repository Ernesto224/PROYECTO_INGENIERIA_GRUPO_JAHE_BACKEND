using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;

namespace Aplicacion.Interfaces
{
    public interface IPublicidadServicio
    {
        public Task<RespuestaDTO<PublicidadDTO>> ModificarPublicidad(int idPublicidad, PublicidadCrearDTO publicidadModificarDTO);
        public Task<PublicidadDTO> VerPublicidadPorId(int id);
        public Task<List<PublicidadDTO>> VerPublicidadesActivas();

        public Task<RespuestaDTO<PublicidadDTO>> EliminarPublicidad(int idOferta);

        public Task<RespuestaDTO<PublicidadDTO>> CrearPublicidad(PublicidadCrearDTO publicidadCrearDTO);
    }
}