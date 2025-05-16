using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Dominio.Entidades
{
    [Table("Reserva")]
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid IdReserva { get; set; }

        [Required]
        public DateTime FechaLlegada { get; set; }

        [Required]
        public DateTime FechaSalida { get; set; }

        [Required]
        public string Estado { get; set; } = string.Empty;

        public bool Activa { get; set; } = true;

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public Cliente? Cliente { get; set; }  

        [ForeignKey("Habitacion")]
        public int IdHabitacion { get; set; }

        public Habitacion? Habitacion { get; set; } = null;

        [ForeignKey("Transaccion")]
        public int IdTransaccion { get; set; }

        public Transaccion? Transaccion { get; set; } = null;

        public decimal CalcularMontoBase(
        Habitacion habitacion,
        DateTime fechaLlegada,
        DateTime fechaSalida)
        {
            TimeSpan diferencia = fechaSalida - fechaLlegada;
            int dias = (int)diferencia.TotalDays;
            return habitacion.TipoDeHabitacion.TarifaDiaria * dias;
        }
    }
}
