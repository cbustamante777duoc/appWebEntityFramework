using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;


namespace appWebEntityFramework.Controllers
{
    public class ViajeController : Controller
    {
        // GET: Viaje
        public ActionResult Index()
        {
            List<ViajeCLS> listaViaje = null;

            using (var bd = new BDPasajeEntities())
            {
                listaViaje = (from viaje in bd.Viaje
                              join lugarOrigen in bd.Lugar
                              on viaje.IIDLUGARORIGEN equals lugarOrigen.IIDLUGAR
                              join lugarDestino in bd.Lugar
                              on viaje.IIDLUGARDESTINO equals lugarDestino.IIDLUGAR
                              join bus in bd.Bus
                              on viaje.IIDBUS equals bus.IIDBUS
                              where viaje.BHABILITADO == 1
                              select new ViajeCLS
                              {
                                  iidViaje = viaje.IIDVIAJE,
                                  nombreBus = bus.PLACA,
                                  nombreLugarOrigen = lugarOrigen.NOMBRE,
                                  nombreLugarDestino = lugarDestino.NOMBRE

                              }).ToList();
                             

            }
            return View(listaViaje);
        }

        public ActionResult Agregar() 
        {
            listarCombos();
            return View();
        }


        /*este metodo se usa para llenar combo lugar de origen y destino */
        public void llenarComboLugar()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Lugar
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.NOMBRE,
                             Value = item.IIDLUGAR.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaLugar = lista;
            }

        }


        public void llenarComboBus()
        {
            List<SelectListItem> lista;

            using (var bd = new BDPasajeEntities())
            {
                lista = (from item in bd.Bus
                         where item.BHABILITADO == 1
                         select new SelectListItem
                         {
                             Text = item.PLACA,
                             Value = item.IIDBUS.ToString()

                         }).ToList();

                lista.Insert(0, new SelectListItem { Text = "---selecione--", Value = "" });
                ViewBag.listaBus = lista;
            }

        }


        public void listarCombos() 
        {

            llenarComboBus();
            llenarComboLugar();
        }
    }
}