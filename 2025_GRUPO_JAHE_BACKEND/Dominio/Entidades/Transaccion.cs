﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidades
{
    [Table("Transaccion")]
    public class Transaccion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTransaccion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public static Transaccion Crear(decimal monto, string descripcion)
        {
            if (monto <= 0) throw new ArgumentException("Monto inválido");

            return new Transaccion
            {
                Monto = monto,
                Descripcion = descripcion,
                Fecha = DateTime.UtcNow
            };
        }
    }
}
