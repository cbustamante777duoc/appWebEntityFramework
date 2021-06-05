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
        [Required]
        [StringLength(100,ErrorMessage ="la longuitud maxima es de 100 caracteres")]
        public string nombre { get; set; }

        [Display(Name = "direcion surcursal")]
        [Required]
        [StringLength(200, ErrorMessage = "la longuitud maxima es de 200 caracteres")]
        public string direccion { get; set; }

        [Display(Name = "telefono surcursal")]
        [Required]
        [StringLength(10, ErrorMessage = "la longuitud maxima es de 10 caracteres")]
        public string telefono { get; set; }

        [Display(Name = "email surcursal")]
        [Required]
        [StringLength(100, ErrorMessage = "la longuitud maxima es de 100 caracteres")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        public string email { get; set; }

        [Display(Name = "fecha de apertura")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode = true)]
        public DateTime fechaApertura { get; set; }

        public int bhabilitado { get; set; }

        public string mensajeError { get; set; }
    }
}