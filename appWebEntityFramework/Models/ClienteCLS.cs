using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appWebEntityFramework.Models
{
    public class ClienteCLS
    {
        [Display(Name ="Id cliente")]
        public int iidcliente { get; set; }

        [Display(Name = "Nombre cliente")]
        [StringLength(100,ErrorMessage ="longuitud maxima de caracteres es de 100")]
        [Required]
        public string nombre { get; set; }

        [Display(Name = "Apellido paterno ")]
        [StringLength(150, ErrorMessage = "longuitud maxima de caracteres es de 150")]
        [Required]
        public string apPaterno { get; set; }

        [Display(Name = "Apellido materno")]
        [StringLength(150, ErrorMessage = "longuitud maxima de caracteres es de 150")]
        [Required]
        public string apMaterno { get; set; }

        [Display(Name = "Email ")]
        [StringLength(200, ErrorMessage = "longuitud maxima de caracteres es de 200")]
        [EmailAddress(ErrorMessage ="Ingrese un email valido")]
        [Required]
        public string email { get; set; }

        [Display(Name = "Direccion")]
        [StringLength(200, ErrorMessage = "longuitud maxima de caracteres es de 200")]
        [Required]
        public string direccion { get; set; }

        [Display(Name = "Sexo ")]
        [Required]
        public int iidsexo { get; set; }

        [Display(Name = "telefono fijo")]
        [StringLength(10, ErrorMessage = "longuitud maxima de caracteres es de 10")]
        [Required]
        public string telefonoFijo { get; set; }

        [Display(Name = "telefono celular")]
        [StringLength(10, ErrorMessage = "longuitud maxima de caracteres es de 10")]
        [Required]
        public string telefonoCelular { get; set; }

        public int bhabilitado { get; set; }


    }
}