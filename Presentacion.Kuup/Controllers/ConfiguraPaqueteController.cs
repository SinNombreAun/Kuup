using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Linq.Dynamic;

namespace Presentacion.Kuup.Controllers
{
    public class ConfiguraPaqueteController : BaseController
    {
        readonly short NumeroDePantalla = (new ClsConfiguraPaquetes()).NumeroDePantallaKuup;
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ACCESO))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Json()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][data]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = 0;

            //var Producto 
            var query = (from q in ClsConfiguraPaquetes.getList()
                         select new
                         {
                             q.CodigoDeBarrasPadre,
                             q.NombreDeProductoPadre,
                             q.PrecioDeProductoPadre,
                             q.CodigoDeBarrasHijo,
                             q.NombreDeProductoHijo,
                             q.PrecioDeProductoHijo,
                             q.ImporteTotal
                         }).AsQueryable();
            if (!String.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.CodigoDeBarrasPadre.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) ||
                x.NombreDeProductoPadre.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) ||
                x.CodigoDeBarrasHijo.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) ||
                x.NombreDeProductoHijo.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
            }
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);
            }
            recordsTotal = query.Count();

            var ConfiguraPaquete = query.Skip(skip).Take(pageSize).ToArray();
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = ConfiguraPaquete }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Alta()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return RedirectToAction("Index", "ConfiguraPaquete");
            }
            return View();
        }
        public JsonResult AutoCompleteProducto(String Prefix)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix), JsonRequestBehavior.AllowGet);
        }
    }
}
