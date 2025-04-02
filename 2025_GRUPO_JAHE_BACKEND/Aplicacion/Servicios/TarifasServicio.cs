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
    public class TarifasServicio : ITarifasServicio
    {

        private readonly ITarifasRepositorio _tarifasRepositorio;

        public TarifasServicio(ITarifasRepositorio tarifasRepositorio)
        {
            this._tarifasRepositorio = tarifasRepositorio;
        }

        public async Task<IEnumerable<TipoDeHabitacionDTO>> verTarifas()
        {
            var tarifas = await this._tarifasRepositorio.verTarifas();

            if (tarifas == null || !tarifas.Any())
                throw new Exception("No se encontraron datos.");

            return tarifas.Select(tipo => new TipoDeHabitacionDTO
            {
                IdTipoDeHabitacion = tipo.IdTipoDeHabitacion,
                Nombre = tipo.Nombre,
                Descripcion = tipo.Descripcion,
                TarifaDiaria = tipo.TarifaDiaria,
                Imagen = tipo.Imagen == null ? null : new ImagenDTO
                {
                    IdImagen = tipo.Imagen.IdImagen,
                    Url = tipo.Imagen.Url
                }
            });

        }
    }
}
