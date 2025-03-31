using Aplicacion.DTOs;
using Aplicacion.Interfaces;
using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Servicios
{
    public class ContactoServicio : IContactoServicio
    {
        private readonly IContactoRepositorio _contactoRepositorio;
        public ContactoServicio(IContactoRepositorio contactoRepositorio)
        {
            _contactoRepositorio = contactoRepositorio;
        }
        public async Task<ContactoDTO> VerDatosContacto()
        {
            var contacto = await _contactoRepositorio.VerDatosContacto();

            if (contacto == null)
                throw new Exception("No se encontraron datos.");

            return new ContactoDTO
            {
                IdContacto = contacto.IdContacto,
                Telefono1 = contacto.Telefono1,
                Telefono2 = contacto.Telefono2,
                ApartadoPostal = contacto.ApartadoPostal,
                Email = contacto.Email
            };
        }
    }
}
