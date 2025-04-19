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

        [Required]
        public bool Activo { get; set; } = false;

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public Cliente? Cliente { get; set; }  

        [ForeignKey("Habitacion")]
        public int IdHabitacion { get; set; }

        public Habitacion? Habitacion { get; set; } = null;

        [ForeignKey("Transaccion")]
        public int IdTransaccion { get; set; }

        public Transaccion? Transaccion { get; set; } = null;
    }
}
