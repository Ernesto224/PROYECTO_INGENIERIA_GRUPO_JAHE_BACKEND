using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.DTOs
{
    public class ContactoDTO
    {
        public int IdContacto { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string ApartadoPostal { get; set; }
        public string Email { get; set; }
    }
}
