using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplicacion.DTOs;


namespace Aplicacion.Interfaces
{
    public interface IReservaServicio
    {
        public Task<bool> RealizarReserva(List<ReservaDTO> reservaDTO, ClienteDTO clienteDTO);

        public Task<HabitacionDTO> VerHabitacionDisponible(ReservaDTO reservaDTO);

        public Task<bool> CambiarEstadoHabitacion(int idHabitacion, string estadoNuevo);

        public Task<List<TipoDeHabitacionDTO>> VerTiposDeHabitacion();
    }
}
