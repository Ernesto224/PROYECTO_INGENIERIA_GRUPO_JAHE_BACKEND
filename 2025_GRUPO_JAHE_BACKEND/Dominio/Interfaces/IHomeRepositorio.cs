using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Interfaces
{
    public interface IHomeRepositorio
    {
        public Task<Home> VerDatosDeHome();
        public Task<object> ModificarDatosDeHome(Home home);
    }
}
