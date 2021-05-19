using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class ViajeCLS
    {
        [Display(Name ="Id  viaje")]
        public int iidViaje { get; set; }

        [Display(Name = "lugar de origen")]
        [Required]
        public int iidLugarOrigen { get; set; }

        [Display(Name = "lugar de destino")]
        [Required]
        public int iidLugarDestino { get; set; }

        [Display(Name = "Precio")]
        [Required]
        public double precio { get; set; }

        [Display(Name = "Fecha viaje")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fechaViaje { get; set; }

        [Display(Name = "Bus")]
        [Required]
        public int iidBus { get; set; }

        [Display(Name = "Numero de asientos disponibles")]
        [Required]
        public int numeroAsientosDisponible { get; set; }


        public int bhabilitado { get; set; }


        //PROPIEDADES ADICIONALES

        [Display(Name = "Lugar origen")]
        public string nombreLugarOrigen { get; set; }

        [Display(Name = "Lugar destino")]
        public string nombreLugarDestino { get; set; }

        [Display(Name = "Nombre bus")]
        public string nombreBus { get; set; }
    }
}           
