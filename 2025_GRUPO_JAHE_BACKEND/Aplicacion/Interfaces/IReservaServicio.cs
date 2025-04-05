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
        public Task<bool> RealizarReserva(ReservaDTO reservaDTO, ClienteDTO clienteDTO, HabitacionDTO habitacionDTO);

        public Task<HabitacionDTO> VerHabitacionDisponible(ReservaDTO);
    }
}
