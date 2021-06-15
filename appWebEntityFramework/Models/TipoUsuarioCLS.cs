using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class TipoUsuarioCLS
    {
        [Display(Name ="id tipo usuario")]
        public int iidTipousuario { get; set; }

        [Display(Name = "tipo nombre usuario")]
        [Required]
        [StringLength(150,ErrorMessage ="Longuitud maxima 150")]
        public string nombre { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(250, ErrorMessage = "Longuitud maxima 250")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}