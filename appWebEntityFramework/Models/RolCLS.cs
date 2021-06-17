using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class RolCLS
    {
        [Display(Name ="Id rol")]
        public int iidRol { get; set; }
        [Required]
        [Display(Name = "Nombre rol")]
        public string nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion rol")]
        public string descripcion { get; set; }
        public int bhabilitado { get; set; }


    }
}