using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidades;

namespace Aplicacion.DTOs
{
    public class PublicidadDTO
    {
        public int IdPublicidad { get; set; }

        public string EnlacePublicidad { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;

        public ImagenDTO? Imagen { get; set; }
    }
}
