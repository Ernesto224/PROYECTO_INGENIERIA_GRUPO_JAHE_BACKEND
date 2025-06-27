using Aplicacion.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface ITemporadaServicio
    {
        Task<RespuestaDTO<TemporadaDTO>> ModificarTemporadaAlta(TemporadaDTO temporadaDTO);


        Task<TemporadaDTO> ObtenerTemporadaAlta();
    }
}
