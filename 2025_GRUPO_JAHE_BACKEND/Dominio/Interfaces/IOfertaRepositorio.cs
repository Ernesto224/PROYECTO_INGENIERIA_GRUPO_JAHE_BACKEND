using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Nucleo;

namespace Dominio.Interfaces
{
    public interface IOfertaRepositorio : IBaseRepositorio<Oferta>
    {

        public Task<Oferta> VerOfertaPorId(int idOferta);

        public Task<IEnumerable<Oferta>> VerOfertasActivas();

        public Task<(IEnumerable<Oferta> ofertas, int datosTotales, int paginaActual)> VerOfertas(int numeroDePagina, int maximoDeDatos, bool irALaUltimaPagina);

        //public Task<Oferta> VerOfertaPorFecha(int idTipoHabitacion, DateTime fechaInicio, DateTime fechaFinal);
    }
}
