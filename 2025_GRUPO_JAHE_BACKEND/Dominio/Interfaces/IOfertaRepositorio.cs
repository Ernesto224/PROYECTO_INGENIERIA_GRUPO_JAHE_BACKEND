using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;
using Dominio.Nucleo;

namespace Dominio.Interfaces
{
    public interface IOfertaRepositorio : IRepositorio<Oferta>
    {
        //public Task<List<Oferta>> VerOfertasActivas();

        //public Task<Oferta> VerOfertaPorFecha(int idTipoHabitacion, DateTime fechaInicio, DateTime fechaFinal);
    }
}
