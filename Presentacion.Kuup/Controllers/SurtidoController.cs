using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class SurtidoController : BaseController
    {
        readonly ClsOperaSurtido Opera = new ClsOperaSurtido();
        readonly short NumeroDePantalla = (new ClsSurtidos()).NumeroDePantallaKuup;
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ACCESO))
            {
                return RedirectToAction("Index","Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Json()
        {
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            var pageSize = length != null ? Convert.ToInt32(length) : 0;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = 0;

            //var Producto 
            var query = (from q in ClsSurtidos.getList()
                         select new
                         {
                             q.FolioDeSurtido,
                             q.NombreDeProducto,
                             NombreDeQuienSurtio = (q.NombreDeProveedor == null ? q.NombreDeUsuario : q.NombreDeProveedor),
                             q.CantidadNueva,
                             FechaDeSurtido = q.FechaDeSurtido.ToString("yyyy-MM-dd"),
                             q.TextoDeEstatus
                         }).AsQueryable();
            if (!String.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.NombreDeProducto.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) || 
                x.NombreDeQuienSurtio.Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) || 
                x.FechaDeSurtido.ToString().Trim().ToUpper().Contains(searchValue.Trim().ToUpper()) ||
                x.TextoDeEstatus.ToString().Trim().ToUpper().Contains(searchValue.Trim().ToUpper()));
            }
            recordsTotal = query.Count();

            var Surtido = query.Skip(skip).Take(pageSize).ToArray();
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data = Surtido }, JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Index", "Surtido");
            }
            SurtidoModel Entidad = new SurtidoModel();
            this.CargaCombos(Entidad);
            return View();
        }
        public JsonResult Alta(String ProductosSurtidos)
        {
            if (!ValidaSesion())
            {
                return Json(new { UrlAccount = Url.Action("LoginOut", "Account") }, JsonRequestBehavior.AllowGet);
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return Json(new { UrlFun = Url.Action("Index", "Surtido") }, JsonRequestBehavior.AllowGet);
            }
            ClsAdicional.ClsResultado Resultado = Opera.InsertSurtidos(ProductosSurtidos);
            return Json(new { Resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteProducto(String Prefix)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto, short NumeroDeProducto = 0)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            List<ClsProductos> Productos = new List<ClsProductos>();
            ClsProductos Producto = new ClsProductos();
            if (NumeroDeProducto == 0)
            {
                Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).ToList();
                if (Productos.Count() == 0)
                {
                    Productos = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).ToList();
                }
            }
            else
            {
                Productos = (from q in ClsProductos.getList() where q.NumeroDeProducto == NumeroDeProducto select q).ToList();
            }
            if (Productos.Count() == 0)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar el producto";
            }
            else
            {
                Producto = Productos.FirstOrDefault();
            }
            return Json(new { Resultado, Producto, Productos }, JsonRequestBehavior.AllowGet);
        }
        private void CargaCombos(SurtidoModel Entidad)
        {
            ViewBag.CveDeAplicaSurtido = ClsAdicional.ClsCargaCombo.CargaComboClave(4,Entidad.CveDeAplicaSurtido.ToString());
            ViewBag.CveDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveDeEstatus.ToString());
        }
    }
}