using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class EmpleadoCLS
    {
        [Display(Name ="Id empleado")]
        
        public int iidEmpleado { get; set; }

        [Display(Name = "Nombre")]
        [Required]
        [StringLength(100,ErrorMessage ="longuitud maxima de 100 caracteres")]
        public string  nombre{ get; set; }

        [Display(Name = "Apellido paterno")]
        [Required]
        [StringLength(200, ErrorMessage = "longuitud maxima de 200 caracteres")]
        public string apPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        [Required]
        [StringLength(200, ErrorMessage = "longuitud maxima de 200 caracteres")]
        public string apMaterno { get; set; }


        [Display(Name = "fecha contrato")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaContrato { get; set; }

        [Display(Name = "sueldo")]
        [Required]
        [Range(0,10000000,ErrorMessage ="Fuera de rango")]
        public decimal sueldo { get; set; }

        [Display(Name = "Tipo usuario")]
        [Required]
        public int iidtipoUsuario { get; set; }

        [Display(Name = "Tipo contrato")]
        [Required]
        public int iidtipoContrato { get; set; }

        [Display(Name = "sexo")]
        [Required]
        public int iidSexo { get; set; }

        public int bhabilitado { get; set; }


        //PROPIEDADES ADICIONALES
        [Display(Name = "nombre del contrato")]
        public string nombreTipoContrato { get; set; }

        [Display(Name = "nombre del tipo usuario ")]
        public string nombreTipoUsuario { get; set; }

        //PROPIEDAD MENSAJE ERROR
        public string mensajeError { get; set; }
    }
}