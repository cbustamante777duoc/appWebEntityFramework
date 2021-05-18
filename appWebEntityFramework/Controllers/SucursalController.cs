using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult Index()
        {
            List<SucursalCLS> listaSucursal = null;
            using (var bd = new BDPasajeEntities())
            {
                listaSucursal = (from sucursal in bd.Sucursal
                                 where sucursal.BHABILITADO==1
                                 select new SucursalCLS
                                 {
                                     iidsucursal = sucursal.IIDSUCURSAL,
                                     nombre = sucursal.NOMBRE,
                                     telefono = sucursal.TELEFONO,
                                     email = sucursal.EMAIL

                                 }).ToList();

            }
            return View(listaSucursal);
        }

        public ActionResult Agregar() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(SucursalCLS oSucursalCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oSucursalCLS);

            }

            using (var bd = new  BDPasajeEntities ())
            {
                Sucursal sucursal = new Sucursal();
                sucursal.NOMBRE = oSucursalCLS.nombre;
                sucursal.DIRECCION = oSucursalCLS.direccion;
                sucursal.TELEFONO = oSucursalCLS.telefono;
                sucursal.EMAIL = oSucursalCLS.email;
                sucursal.FECHAAPERTURA = oSucursalCLS.fechaApertura;
                sucursal.BHABILITADO = 1;
                bd.Sucursal.Add(sucursal);
                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}