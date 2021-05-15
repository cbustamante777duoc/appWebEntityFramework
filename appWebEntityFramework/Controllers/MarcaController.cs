using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            List<MarcaCLS> listaMarca = null;
            using (var bd = new BDPasajeEntities())
            {
                listaMarca = (from marca in bd.Marca
                              where marca.BHABILITADO == 1
                              select new MarcaCLS
                              {
                                  iidmarca = marca.IIDMARCA,
                                  nombre = marca.NOMBRE,
                                  descripcion = marca.DESCRIPCION
                              }).ToList();
            }

            return View(listaMarca);
        }

        public ActionResult Agregar() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar( MarcaCLS oMarcaCLS)
        {
            if (!ModelState.IsValid)
            {
                return View(oMarcaCLS);

            }
            else
            {
                using (var bd = new BDPasajeEntities())
                {
                    Marca marca = new Marca();
                    marca.IIDMARCA = oMarcaCLS.iidmarca;
                    marca.NOMBRE = oMarcaCLS.nombre;
                    marca.DESCRIPCION = oMarcaCLS.descripcion;
                    marca.BHABILITADO = 1;
                    bd.Marca.Add(marca);
                    bd.SaveChanges();

                }

            }
            return RedirectToAction("Index");
        }
    }
}