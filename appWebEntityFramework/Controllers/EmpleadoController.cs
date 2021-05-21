using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class EmpleadoController : Controller
    {


        List<EmpleadoCLS> listaEmpleado = null;
        public ActionResult Index()
        {
            using (var bd = new BDPasajeEntities())
            {
                listaEmpleado = (from empleado in bd.Empleado
                                 join tipoUsuario in bd.TipoUsuario
                                 on empleado.IIDTIPOUSUARIO equals tipoUsuario.IIDTIPOUSUARIO
                                 join tipoContrato in bd.TipoContrato
                                 on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                                 where empleado.BHABILITADO ==1
                                 select new EmpleadoCLS
                                 {
                                     iidEmpleado = empleado.IIDEMPLEADO,
                                     nombre = empleado.NOMBRE,
                                     apPaterno = empleado.APPATERNO,
                                     nombreTipoContrato = tipoContrato.NOMBRE,
                                     nombreTipoUsuario = tipoUsuario.NOMBRE

                                 }).ToList();

            }
            return View(listaEmpleado);
        }


        public void llenarComboSexo() 
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from sexo in bd.Sexo
                         where sexo.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = sexo.NOMBRE,
                             Value = sexo.IIDSEXO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaSexo = lista;
            }
        
        }

        public void llenarComboTipoContrato()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from tipoContrato in bd.TipoContrato
                         where tipoContrato.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = tipoContrato.NOMBRE,
                             Value = tipoContrato.IIDTIPOCONTRATO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaTipoContrato = lista;
            }

        }

        public void llenarComboTipoUsuario()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from TipoUsuario in bd.TipoUsuario
                         where TipoUsuario.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = TipoUsuario.NOMBRE,
                             Value = TipoUsuario.IIDTIPOUSUARIO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaTipoUsuario = lista;
            }

        }

        public void listarCombos() 
        {
            llenarComboSexo();
            llenarComboTipoContrato();
            llenarComboTipoUsuario();
        }

        public ActionResult Agregar() 
        {
            listarCombos();

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(EmpleadoCLS oEmpleadoCLS) 
        {
            if (!ModelState.IsValid)
            {
                //importar los combox
                listarCombos();
                return View(oEmpleadoCLS);

            }

            using (var bd = new BDPasajeEntities())
            {
                Empleado empleado = new Empleado();
                empleado.NOMBRE = oEmpleadoCLS.nombre;
                empleado.APPATERNO = oEmpleadoCLS.apPaterno;
                empleado.APMATERNO = oEmpleadoCLS.apMaterno;
                empleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                empleado.SUELDO = oEmpleadoCLS.sueldo;
                empleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                empleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                empleado.IIDSEXO = oEmpleadoCLS.iidSexo;
                empleado.BHABILITADO = 1;

                bd.Empleado.Add(empleado);
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}