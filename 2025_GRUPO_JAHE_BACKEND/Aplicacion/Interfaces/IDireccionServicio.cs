using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IDireccionServicio
    {
        public Task<DireccionDTO> VerDatosDireccion();
        public Task<DireccionDTO> CambiarTextoComoLlegar(DireccionDTO direccion);
    }
}
