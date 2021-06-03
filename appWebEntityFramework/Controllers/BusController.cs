using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class BusController : Controller
    {


        public ActionResult Index()
        {
             List<BusCLS> listaBus = null;
            using (var bd = new BDPasajeEntities())
            {
                listaBus = (from bus in bd.Bus
                            join sucursal in bd.Sucursal
                            on bus.IIDSUCURSAL equals sucursal.IIDSUCURSAL
                            join tipoBus in bd.TipoBus
                            on bus.IIDTIPOBUS equals tipoBus.IIDTIPOBUS
                            join tipoModelo in bd.Modelo
                            on bus.IIDMODELO equals tipoModelo.IIDMODELO
                            where bus.BHABILITADO==1
                            select new BusCLS
                            {
                                iidBus = bus.IIDBUS,
                                placa = bus.PLACA,
                                nombreModelo = tipoModelo.NOMBRE,
                                nombreSucursal = sucursal.NOMBRE,
                                nombreTipoBus = tipoBus.NOMBRE,

                            }).ToList();
            }

            return View(listaBus);
        }

        public void llenarComboTipoBus()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.TipoBus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDTIPOBUS.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaTipoBus = lista;
            }

        }

        public void llenarComboMarca()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Marca
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMARCA.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaMarca = lista;
            }

        }

        public void llenarComboModelo()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Modelo
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDMODELO.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaModelo = lista;
            }

        }

        public void llenarComboSucursal()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Sucursal
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDSUCURSAL.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaSurcursal = lista;
            }

        }

        public void ListarComboxes()
        {
            llenarComboSucursal();
            llenarComboMarca();
            llenarComboModelo();
            llenarComboTipoBus();
        }


        public ActionResult Agregar() 
        {
            ListarComboxes();
            return View();
        }


        [HttpPost]
        public ActionResult Agregar(BusCLS oBusCLS) 
        {
            if (!ModelState.IsValid)
            {
                ListarComboxes();
                return View(oBusCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Bus bus = new Bus();
                bus.IIDSUCURSAL = oBusCLS.iidSucursal;
                bus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                bus.PLACA = oBusCLS.placa;
                bus.FECHACOMPRA= oBusCLS.fechaCompra;
                bus.IIDMODELO= oBusCLS.iidModelo;
                bus.NUMEROFILAS= oBusCLS.numeroFilas;
                bus.NUMEROCOLUMNAS= oBusCLS.numeroColumnas;
                bus.DESCRIPCION= oBusCLS.descripcion;
                bus.OBSERVACION= oBusCLS.observacion;
                bus.IIDMARCA= oBusCLS.iidmarca;
                bus.BHABILITADO = 1;

                bd.Bus.Add(bus);
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }

        public ActionResult Editar(int id) 
        {
            BusCLS oBusCLS = new BusCLS();
            ListarComboxes();

            using (var bd = new BDPasajeEntities())
            {
                Bus oBus = bd.Bus.Where(p => p.IIDBUS.Equals(id)).First();
                oBusCLS.iidBus = oBus.IIDBUS;
                oBusCLS.iidSucursal =(int) oBus.IIDSUCURSAL;
                oBusCLS.iidTipoBus =(int) oBus.IIDTIPOBUS;
                oBusCLS.placa = oBus.PLACA;
                oBusCLS.fechaCompra = (DateTime) oBus.FECHACOMPRA;
                oBusCLS.iidModelo =(int) oBus.IIDMODELO;
                oBusCLS.numeroColumnas =(int) oBus.NUMEROCOLUMNAS;
                oBusCLS.numeroFilas = (int) oBus.NUMEROFILAS;
                oBusCLS.descripcion =  oBus.DESCRIPCION;
                oBusCLS.observacion =  oBus.OBSERVACION;
                oBusCLS.iidmarca = (int)  oBus.IIDMARCA;
            }

            return View(oBusCLS);
        
        }


        [HttpPost]
        public ActionResult Editar(BusCLS oBusCLS) 
        {
            int idBus = oBusCLS.iidBus;

            if (!ModelState.IsValid)
            {
                return View(oBusCLS);
            }

            using (var bd = new BDPasajeEntities())
            {
                Bus bus = bd.Bus.Where(p => p.IIDBUS.Equals(idBus)).First();

                bus.IIDSUCURSAL = oBusCLS.iidSucursal;
                bus.IIDTIPOBUS = oBusCLS.iidTipoBus;
                bus.PLACA = oBusCLS.placa;
                bus.FECHACOMPRA = oBusCLS.fechaCompra;
                bus.IIDMODELO = oBusCLS.iidModelo;
                bus.NUMEROCOLUMNAS = oBusCLS.numeroColumnas;
                bus.NUMEROFILAS = oBusCLS.numeroFilas;
                bus.DESCRIPCION = oBusCLS.descripcion;
                bus.OBSERVACION = oBusCLS.observacion;
                bus.IIDMARCA = oBusCLS.iidmarca;

                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        
        }
     

    }
}