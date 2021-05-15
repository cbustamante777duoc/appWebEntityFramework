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
        [Required]
        [StringLength(100, ErrorMessage = "la longitud maxima es de 100")]
        public string nombre{ get; set; }

        [Display(Name = "descripcion marca")]
        [Required]
        [StringLength(200,ErrorMessage ="la longitud maxima es de 200")]
        public string descripcion { get; set; }

        public int bhabilitado { get; set; }
    }
}