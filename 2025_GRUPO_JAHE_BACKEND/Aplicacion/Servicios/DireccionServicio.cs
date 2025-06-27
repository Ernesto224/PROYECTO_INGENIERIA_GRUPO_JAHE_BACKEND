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
    public class DireccionServicio : IDireccionServicio
    {
        private readonly IDireccionRepositorio _direccionRepositorio;
        public DireccionServicio(IDireccionRepositorio direccionRepositorio)
        {
            this._direccionRepositorio = direccionRepositorio;
        }

        public async Task<DireccionDTO> CambiarTextoComoLlegar(DireccionDTO direccion)
        {
            var direccionActualizada = await _direccionRepositorio.CambiarTextoComoLlegar(new Direccion
            {
                IdDireccion = direccion.IdDireccion,
                Descripcion = direccion.Descripcion,
            });

            return new DireccionDTO
            {
                IdDireccion = direccionActualizada.IdDireccion,
                Descripcion = direccionActualizada.Descripcion
            };
        }

        public async Task<DireccionDTO> VerDatosDireccion()
        {
            var direccion = await _direccionRepositorio.VerDatosDireccion();
            return new DireccionDTO
            {
                IdDireccion = direccion.IdDireccion,
                Descripcion = direccion.Descripcion,
            };
        }
    }
}
