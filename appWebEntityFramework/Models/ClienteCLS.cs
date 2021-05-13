using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class ClienteCLS
    {
        [Display(Name ="Id cliente")]
        public int iidcliente { get; set; }

        [Display(Name = "Nombre cliente")]
        public string nombre { get; set; }

        [Display(Name = "Apellido paterno ")]
        public string apPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        public string apMaterno { get; set; }

        public string email { get; set; }

        public string direccion { get; set; }

        public int iidsexo { get; set; }

        [Display(Name = "telefono fijo")]
        public string telefonoFijo { get; set; }

        public string telefonoCelular { get; set; }

        public int bhabilitado { get; set; }


    }
}