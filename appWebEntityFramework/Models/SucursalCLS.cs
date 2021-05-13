using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class SucursalCLS
    {
        [Display(Name ="Id surcursal")]
        public int iidsucursal { get; set; }

        [Display(Name = "nombre surcursal")]
        public string nombre { get; set; }

        [Display(Name = "direcion surcursal")]
        public string direccion { get; set; }

        [Display(Name = "telefono surcursal")]
        public string telefono { get; set; }

        [Display(Name = "email surcursal")]
        public string email { get; set; }

        public DateTime fechaApertura { get; set; }

        public int bhabilitado { get; set; }
    }
}