using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class PaginaCLS
    {
        [Display(Name ="Id pagina")]
        public int iidpagina { get; set; }

        [Required]
        [Display(Name = "Titulo del link")]
        public string mensaje { get; set; }

        [Required]
        [Display(Name = "Nombre de la accion")]
        public string accion { get; set; }

        [Required]
        [Display(Name = "Nombre del controlador")]
        public string controlador { get; set; }
        public int bhabilitado { get; set; }


    }
}