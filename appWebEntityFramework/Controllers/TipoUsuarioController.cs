using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using appWebEntityFramework.Models;

namespace appWebEntityFramework.Controllers
{
    public class TipoUsuarioController : Controller
    {
        private TipoUsuarioCLS otipoVal;

        private bool buscarTipoUsuario(TipoUsuarioCLS tipoUsuarioCLS) 
        {
            bool busquedaId = true;
            bool busquedaNombre = true;
            bool busquedaDescripcion = true;

            if (otipoVal.iidTipousuario>0)
            {
                busquedaId = tipoUsuarioCLS.iidTipousuario.ToString().Contains(otipoVal.iidTipousuario.ToString());

            }

            if (otipoVal.nombre != null)
            {
               busquedaNombre = tipoUsuarioCLS.nombre.ToString().Contains(otipoVal.nombre);
            }

            if (otipoVal.descripcion != null)
            {
                busquedaDescripcion = tipoUsuarioCLS.descripcion.ToString().Contains(otipoVal.descripcion);

            }



            return (busquedaId && busquedaNombre && busquedaDescripcion);
        }

        // GET: TipoUsuario
        public ActionResult Index(TipoUsuarioCLS oTipoUsuarioCLS)
        {
            //se igual al modelo
            otipoVal = oTipoUsuarioCLS;

            List<TipoUsuarioCLS> listaTipoUsuario = null;

            List<TipoUsuarioCLS> listaFiltrada;

            using (var bd = new BDPasajeEntities())
            {
                listaTipoUsuario = (from tipoUsuario in bd.TipoUsuario
                                    where tipoUsuario.BHABILITADO == 1
                                    select new TipoUsuarioCLS
                                    {
                                        iidTipousuario = tipoUsuario.IIDTIPOUSUARIO,
                                        nombre = tipoUsuario.NOMBRE,
                                        descripcion = tipoUsuario.DESCRIPCION
                                    }).ToList();

                if (oTipoUsuarioCLS.iidTipousuario ==0 && oTipoUsuarioCLS.nombre==null
                   && oTipoUsuarioCLS.descripcion ==null)
                {
                    listaFiltrada = listaTipoUsuario;
                }
                else
                {
                    Predicate<TipoUsuarioCLS> pre = new Predicate<TipoUsuarioCLS>(buscarTipoUsuario);
                    listaFiltrada =  listaTipoUsuario.FindAll(pre);

                }

            }

            return View(listaFiltrada);
        }
    }
}