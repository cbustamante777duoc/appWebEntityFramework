using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class MarcaCLS
    {
        [Display(Name ="Id marca")]
        public int iidmarca { get; set; }

        [Display(Name = "nombre marca")]
        public string nombre{ get; set; }

        [Display(Name = "descripcion marca")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}