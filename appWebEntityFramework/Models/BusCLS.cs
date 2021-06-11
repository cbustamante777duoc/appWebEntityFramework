using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class BusCLS
    {
        [Display(Name ="Id bus")]
        public int iidBus { get; set; }

        [Display(Name = "Nombre sucursal")]
        [Required]
        public int iidSucursal { get; set; }

        [Display(Name = "Tipo bus")]
        [Required]
        public int iidTipoBus { get; set; }

        [Display(Name = "Placa")]
        [Required]
        [StringLength(100,ErrorMessage ="longuitud maxima 100 caracteres")]
        public string placa { get; set; }


        [Display(Name = "Fecha de compra")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaCompra { get; set; }


        [Display(Name = "Nombre modelo")]
        [Required]
        public int iidModelo { get; set; }

        [Display(Name = "Numero de filas")]
        [Required]
        public int numeroFilas { get; set; }

        [Display(Name = "Numero de columnas")]
        [Required]
        public int numeroColumnas { get; set; }

        public int bhabilitado { get; set; }

        [Display(Name = "Descripcion")]
        [Required]
        [StringLength(200, ErrorMessage = "el numero maximo de caracteres es de 200")]
        public string descripcion{ get; set; }

        [Display(Name = "observacion")]
        [StringLength(200,ErrorMessage ="el numero maximo de caracteres es de 200")]

        public string observacion { get; set; }

        [Display(Name = "Nombre marca")]
        [Required]
        public int iidmarca { get; set; }

        //PROPIEDADES ADICIONALES

        [Display(Name = "Nombre sucurasal")]
        public string nombreSucursal{ get; set; }

        [Display(Name = "Nombre tipo bus")]
        public string nombreTipoBus { get; set; }

        [Display(Name = "Nombre modelo")]
        public string nombreModelo { get; set; }

        //PROPIEDAD ADICIONAL MENSAJE ERROR 
        public string mensajeError { get; set; }
    }
}