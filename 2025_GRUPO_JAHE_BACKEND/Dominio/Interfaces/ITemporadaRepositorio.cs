using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Dominio.Interfaces
{
    public interface ITemporadaRepositorio
    {
        Task<Temporada> ObtenerTemporadaPorFecha(DateTime fechaInicio, DateTime fechaFinal);

        Task<Temporada> ObtenerTemporadaAlta();

        Task<Temporada> ModificarTemporada(Temporada temporada);
    }
}
