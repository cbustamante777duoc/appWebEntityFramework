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
                                    apPaterno = cliente.APPATERNO,
                                    apMaterno = cliente.APMATERNO,
                                    telefonoFijo = cliente.TELEFONOFIJO

                                }).ToList();
            }

            return View(listaCliente);
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

        public ActionResult Agregar()
        {
            llenarComboSexo();
            

            return View();
        }

        [HttpPost]
        public ActionResult Agregar(ClienteCLS oClienteCLS) 
        {
            int nRegistrosEncontrados = 0;
            string nombre = oClienteCLS.nombre;
            string apPaterno = oClienteCLS.apPaterno;
            string apMaterno = oClienteCLS.apMaterno;

            //VALIDACION DE QUE NO SE REPITA EN BD EL NOMBRE COMPLETO
            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && 
                p.APPATERNO.Equals(apPaterno) && p.APMATERNO.Equals(apMaterno)).Count();

            }

            if (!ModelState.IsValid || nRegistrosEncontrados>=1)
            {
                if (nRegistrosEncontrados >= 1) oClienteCLS.mensajeError = "ya esta registrado el cliente";

                llenarComboSexo();
                

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


        public ActionResult Editar(int id) 
        {
            ClienteCLS oClienteCLS = new ClienteCLS();

            using (var bd = new BDPasajeEntities())
            {
                llenarComboSexo();
                

                Cliente oCliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(id)).First();

                oClienteCLS.iidcliente = oCliente.IIDCLIENTE;
                oClienteCLS.nombre= oCliente.NOMBRE;
                oClienteCLS.apPaterno= oCliente.APPATERNO;
                oClienteCLS.apMaterno = oCliente.APMATERNO;
                oClienteCLS.direccion = oCliente.DIRECCION;
                oClienteCLS.email= oCliente.EMAIL;
                oClienteCLS.iidsexo= (int) oCliente.IIDSEXO;
                oClienteCLS.telefonoCelular= oCliente.TELEFONOCELULAR;
                oClienteCLS.telefonoFijo= oCliente.TELEFONOFIJO;

            }


            return View(oClienteCLS);
        }

        [HttpPost]
        public ActionResult Editar(ClienteCLS oClienteCLS) 
        {
            int nRegistrosEncontrado = 0;
            int idCliente = oClienteCLS.iidcliente;
            string nombre = oClienteCLS.nombre;
            string appPaterno = oClienteCLS.apPaterno;
            string appMaterno = oClienteCLS.apMaterno;

            using (var bd = new BDPasajeEntities())
            {
               nRegistrosEncontrado = bd.Cliente.Where(p => p.NOMBRE.Equals(nombre) && p.APPATERNO.Equals(appPaterno)
                && p.APMATERNO.Equals(appMaterno) && !p.IIDCLIENTE.Equals(idCliente)).Count();

            }

            if (!ModelState.IsValid || nRegistrosEncontrado>=1)
            {
                if (nRegistrosEncontrado >= 1) oClienteCLS.mensajeError = "Ya existe el cliente";
                //llamar el comboSexo evita que se caiga el programa
                llenarComboSexo();
                return View(oClienteCLS);

            }

            using (var bd = new BDPasajeEntities())
            {
                Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(idCliente)).First();
                cliente.NOMBRE = oClienteCLS.nombre;
                cliente.APPATERNO = oClienteCLS.apPaterno;
                cliente.APMATERNO = oClienteCLS.apMaterno;
                cliente.EMAIL = oClienteCLS.email;
                cliente.DIRECCION = oClienteCLS.direccion;
                cliente.IIDSEXO = oClienteCLS.iidsexo;
                cliente.TELEFONOCELULAR = oClienteCLS.telefonoCelular;
                cliente.TELEFONOFIJO= oClienteCLS.telefonoFijo;
                bd.SaveChanges();
    
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int iidcliente) 
        {
            using (var bd = new  BDPasajeEntities())
            {
                Cliente cliente = bd.Cliente.Where(p => p.IIDCLIENTE.Equals(iidcliente)).First();
                cliente.BHABILITADO = 0;
                bd.SaveChanges();

                return RedirectToAction("Index");

            }
        
        }
    }


}