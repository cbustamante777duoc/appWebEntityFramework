using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            List<ClienteCLS> listaCliente = null;

            using (var bd = new BDPasajeEntities())
            {
                listaCliente = (from cliente in bd.Cliente
                                where cliente.BHABILITADO == 1
                                select new ClienteCLS
                                {
                                    iidcliente = cliente.IIDCLIENTE,
                                    nombre = cliente.NOMBRE,
                                    apPaterno = cliente.APMATERNO,
                                    apMaterno = cliente.APMATERNO,
                                    telefonoFijo = cliente.TELEFONOFIJO

                                }).ToList();
            }

            return View(listaCliente);
        }


        List<SelectListItem> listaSexo;

        private void llenarSexo() 
        {
            using (var bd = new BDPasajeEntities())
            {
                listaSexo = (from sexo in bd.Sexo
                             where sexo.BHABILITADO == 1
                             select new SelectListItem
                             {
                                 Text = sexo.NOMBRE,
                                 Value = sexo.IIDSEXO.ToString()

                             }).ToList();

                listaSexo.Insert(0, new SelectListItem { Text = "-----seleccione----", Value = "" });

            }
        
        }

        public ActionResult Agregar()
        {
            llenarSexo();
            ViewBag.lista = listaSexo;

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS) 
        {
            if (!ModelState.IsValid)
            {
                llenarSexo();
                ViewBag.lista = listaSexo;

                return View(oClienteCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Cliente cliente = new Cliente();
                cliente.NOMBRE = oClienteCLS.nombre;
                cliente.APPATERNO = oClienteCLS.apMaterno;
                cliente.APMATERNO = oClienteCLS.apMaterno;
                cliente.EMAIL = oClienteCLS.email;
                cliente.DIRECCION = oClienteCLS.direccion;
                cliente.TELEFONOFIJO = oClienteCLS.telefonoFijo;
                cliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                cliente.IIDSEXO = oClienteCLS.iidsexo;
                cliente.BHABILITADO = 1;
                bd.Cliente.Add(cliente);
                bd.SaveChanges();


            }

            return RedirectToAction("Index");
        }
    }


}