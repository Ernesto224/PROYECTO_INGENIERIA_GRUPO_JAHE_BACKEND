using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;
using Dominio.Entidades;


namespace Aplicacion.Interfaces
{
    public interface IReservaServicio
    {
        public Task<List<string>> RealizarReserva(List<ReservaDTO> reservaDTO, ClienteDTO clienteDTO);

        public Task<HabitacionDTO> VerHabitacionDisponible(ReservaDTO reservaDTO);

        public Task<IEnumerable<AlternativaDeReservaDTO>> VerAlternativasDisponibles(ReservaDTO reservaDTO);

        public Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo);

        public Task<List<TipoDeHabitacionDTO>> VerTiposDeHabitacion();
    }
}
