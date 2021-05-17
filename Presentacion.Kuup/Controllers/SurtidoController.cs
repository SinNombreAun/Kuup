using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;

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
            this.CargaCombosParaTabla();
            return View();
        }
        [HttpPost]
        public ActionResult Json()
        {
            return Json((new ClsSurtidos()).DataTableSurtido(new Negocio.Kuup.Globales.ClsDataTables(Request.Form)), JsonRequestBehavior.AllowGet);
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
        public JsonResult AutoCompleteProducto(String Prefix, short NumeroDeTipoDeProducto = 0, short NumeroDeMarca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix, NumeroDeTipoDeProducto, NumeroDeMarca), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto, short NumeroDeProducto = 0)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            List<ClsProductos> Productos = new List<ClsProductos>();
            ClsProductos Producto = new ClsProductos();
            if (NumeroDeProducto == 0)
            {
                Productos = ClsProductos.getList(String.Format("CodigoDeBarras == \"{0}\" && CveDeEstatus == {1}",NombreOCodigoDeProducto, (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO));
                if (Productos.Count() == 0)
                {
                    Productos = ClsProductos.getList(String.Format("NombreDeProducto == \"{0}\" && CveDeEstatus == {1}", NombreOCodigoDeProducto,(byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO));
                }
            }
            else
            {
                Productos = ClsProductos.getList(String.Format("NumeroDeProducto == {0} && CveDeEstatus == {1}", NumeroDeProducto, (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO));
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
            ViewBag.NumeroDeTipoDeProducto = ClsAdicional.ClsCargaCombo.CargaComboTipoDeProducto(String.Empty);
            ViewBag.NumeroDeMarca = ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(0, String.Empty);
        }
        private void CargaCombosParaTabla()
        {
            ViewBag.TextoDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClaveParaTabla(1, "TextoDeEstatus");
        }
        public JsonResult ObtenMarcaPorTipo(short TipoDeProducto, short Marca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(TipoDeProducto, Marca.ToString()), JsonRequestBehavior.AllowGet);
        }
    }
}