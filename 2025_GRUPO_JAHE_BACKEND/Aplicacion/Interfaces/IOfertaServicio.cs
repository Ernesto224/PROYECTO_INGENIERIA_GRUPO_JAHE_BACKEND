using Aplicacion.DTOs;
using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Interfaces
{
    public interface IOfertaServicio
    {

        public Task<RespuestaDTO<OfertaDTO>> CrearOferta(OfertaDTO ofertaDTO);

        public Task<RespuestaConsultaDTO<OfertaDTO>> VerOfertas(int NumeroDePagina, int MaximoDeDatos, bool IrALaUltimaPagina);

        public Task<List<OfertaDTO>> VerOfertasActivas();

        public Task<RespuestaDTO<OfertaDTO>> ModificarOferta(OfertaModificarDTO ofertaModificarDTO);

        public Task<RespuestaDTO<OfertaDTO>> EliminarOferta(int idOferta);
    }
}
