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
        public ActionResult Index(MarcaCLS marcaCLS)
        {
            string nombreMarca = marcaCLS.nombre;

            List<MarcaCLS> listaMarca = null;

            using (var bd = new BDPasajeEntities())
            {

                if (marcaCLS.nombre == null)
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
                else
                {

                    listaMarca = (from marca in bd.Marca
                                  where marca.BHABILITADO == 1
                                  && marca.NOMBRE.Contains(nombreMarca)
                                  select new MarcaCLS
                                  {
                                      iidmarca = marca.IIDMARCA,
                                      nombre = marca.NOMBRE,
                                      descripcion = marca.DESCRIPCION
                                  }).ToList();
                }
            }

            return View(listaMarca);
        }

        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Agregar(MarcaCLS oMarcaCLS)
        {
            int nregistroEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;

            using (var bd = new BDPasajeEntities())
            {
                nregistroEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca)).Count();

            }


            //validacion de datos y registros
            if (!ModelState.IsValid || nregistroEncontrados>=1)
            {
                if (nregistroEncontrados >= 1) oMarcaCLS.mensajeError = "El nombre de esta marca ya existe en bd";

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

        public ActionResult Editar(int id) 
        {
            MarcaCLS oMarcaCLS = new MarcaCLS();

            using (var bd = new BDPasajeEntities())
            {
                Marca oMarca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                oMarcaCLS.iidmarca = oMarca.IIDMARCA;
                oMarcaCLS.nombre = oMarca.NOMBRE;
                oMarcaCLS.descripcion = oMarca.DESCRIPCION;

            }

            return View(oMarcaCLS);
        }

        [HttpPost]
        public ActionResult Editar(MarcaCLS oMarcaCLS) 
        {
            int nRegistrosEncontrados = 0;
            string nombreMarca = oMarcaCLS.nombre;
            int idMarcaNveces = oMarcaCLS.iidmarca;

            using (var bd = new BDPasajeEntities())
            {
                nRegistrosEncontrados = bd.Marca.Where(p => p.NOMBRE.Equals(nombreMarca) && !p.IIDMARCA.Equals(idMarcaNveces)).Count();

            }

            if (!ModelState.IsValid || nRegistrosEncontrados >=1)
            {

                if (nRegistrosEncontrados >= 1) oMarcaCLS.mensajeError = "ya se encuentra registrada la marca";
                

                return View(oMarcaCLS);

            }


            int idMarca = oMarcaCLS.iidmarca;

            using (var bd = new BDPasajeEntities())
            {

                Marca marca = bd.Marca.Where(p => p.IIDMARCA.Equals(idMarca)).First();

                marca.NOMBRE = oMarcaCLS.nombre;
                marca.DESCRIPCION = oMarcaCLS.descripcion;
                bd.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int id) 
        {
            using (var bd = new BDPasajeEntities())
            {
                Marca marca = bd.Marca.Where(p => p.IIDMARCA.Equals(id)).First();
                //eliminacion logica
                marca.BHABILITADO = 0;

                bd.SaveChanges();

            }

            return RedirectToAction("Index");
        
        }
    }
}