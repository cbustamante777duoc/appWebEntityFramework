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


        
        public ActionResult Index(EmpleadoCLS oempleadoCLS)
        {
            List<EmpleadoCLS> listaEmpleado = null;
            llenarComboTipoUsuario();
            int iidTipoUsuario = oempleadoCLS.iidtipoUsuario;

            using (var bd = new BDPasajeEntities())
            {
                if (iidTipoUsuario == 0)
                {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipoUsuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipoUsuario.IIDTIPOUSUARIO
                                     join tipoContrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         apPaterno = empleado.APPATERNO,
                                         nombreTipoContrato = tipoContrato.NOMBRE,
                                         nombreTipoUsuario = tipoUsuario.NOMBRE

                                     }).ToList();
                }
                else
                {
                    listaEmpleado = (from empleado in bd.Empleado
                                     join tipoUsuario in bd.TipoUsuario
                                     on empleado.IIDTIPOUSUARIO equals tipoUsuario.IIDTIPOUSUARIO
                                     join tipoContrato in bd.TipoContrato
                                     on empleado.IIDTIPOCONTRATO equals tipoContrato.IIDTIPOCONTRATO
                                     where empleado.BHABILITADO == 1
                                     && empleado.IIDTIPOUSUARIO == iidTipoUsuario
                                     select new EmpleadoCLS
                                     {
                                         iidEmpleado = empleado.IIDEMPLEADO,
                                         nombre = empleado.NOMBRE,
                                         apPaterno = empleado.APPATERNO,
                                         nombreTipoContrato = tipoContrato.NOMBRE,
                                         nombreTipoUsuario = tipoUsuario.NOMBRE

                                     }).ToList();

                }
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
            int nRegistroEncontrado = 0;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;

            using (var bd = new BDPasajeEntities())
            {
                nRegistroEncontrado = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) &&
                p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();

            }

            if (!ModelState.IsValid || nRegistroEncontrado>=1)
            {
                if (nRegistroEncontrado >= 1) oEmpleadoCLS.mensajeError = "El Empleado ya existe en la sistema";

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

        public ActionResult Editar(int id)
        {
            listarCombos();

            EmpleadoCLS oEmpleadoCLS = new EmpleadoCLS();

            using (var bd = new BDPasajeEntities())
            {
                Empleado oEmpleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(id)).First();

                oEmpleadoCLS.nombre = oEmpleado.NOMBRE;
                oEmpleadoCLS.apPaterno= oEmpleado.APPATERNO;
                oEmpleadoCLS.apMaterno= oEmpleado.APMATERNO;
                oEmpleadoCLS.fechaContrato = (DateTime) oEmpleado.FECHACONTRATO;
                oEmpleadoCLS.sueldo = (decimal) oEmpleado.SUELDO;
                oEmpleadoCLS.iidEmpleado = oEmpleado.IIDEMPLEADO;
                oEmpleadoCLS.iidtipoUsuario =(int) oEmpleado.IIDTIPOUSUARIO;
                oEmpleadoCLS.iidtipoContrato =(int) oEmpleado.IIDTIPOCONTRATO;
                oEmpleadoCLS.iidSexo =(int) oEmpleado.IIDSEXO;
            }

            return View(oEmpleadoCLS);
        }

        [HttpPost]
        public ActionResult Editar(EmpleadoCLS oEmpleadoCLS) 
        {
            int idEmpleado = oEmpleadoCLS.iidEmpleado;

            int nRegistrosEncontrado = 0;
            string nombre = oEmpleadoCLS.nombre;
            string apPaterno = oEmpleadoCLS.apPaterno;
            string apMaterno = oEmpleadoCLS.apMaterno;

            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrado = bd.Empleado.Where(p => p.NOMBRE.Equals(nombre) &&
                p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno) && !p.IIDEMPLEADO.Equals(idEmpleado)).Count();

            }
            
            if (!ModelState.IsValid || nRegistrosEncontrado>=1)
            {
                if (nRegistrosEncontrado >= 1) oEmpleadoCLS.mensajeError = "Ya existe el empleado en sistema";
                listarCombos();
                return View(oEmpleadoCLS);
            }

            using (var bd= new BDPasajeEntities())
            {

                Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(idEmpleado)).First();
                empleado.NOMBRE = oEmpleadoCLS.nombre;
                empleado.APPATERNO = oEmpleadoCLS.apPaterno;
                empleado.APMATERNO = oEmpleadoCLS.apMaterno;
                empleado.FECHACONTRATO = oEmpleadoCLS.fechaContrato;
                empleado.SUELDO = oEmpleadoCLS.sueldo;
                empleado.IIDTIPOCONTRATO = oEmpleadoCLS.iidtipoContrato;
                empleado.IIDTIPOUSUARIO = oEmpleadoCLS.iidtipoUsuario;
                empleado.IIDSEXO= oEmpleadoCLS.iidSexo;

                bd.SaveChanges();
            }


            return RedirectToAction("Index");
        
        }

        [HttpPost]
        public ActionResult Eliminar(int txtidEmpleado) 
        {
            using (var bd = new BDPasajeEntities())
            {
                Empleado empleado = bd.Empleado.Where(p => p.IIDEMPLEADO.Equals(txtidEmpleado)).First();
                empleado.BHABILITADO = 0;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        
        }

    }
}