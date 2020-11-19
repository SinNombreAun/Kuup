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
        public ActionResult Index(bool Grid = false)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ACCESO))
            {
                return RedirectToAction("Index","Home");
            }
            if (Grid)
            {
                var Surtido = (from q in ClsSurtidos.getList()
                               select new
                               {
                                   q.FolioDeSurtido,
                                   q.NombreDeProducto,
                                   NombreDeQuienSurtio = (q.NombreDeProveedor == null ? q.NombreDeUsuario : q.NombreDeProveedor),
                                   q.CantidadNueva,
                                   FechaDeSurtido = q.FechaDeSurtido.ToString("yyyy-MM-dd"),
                                   q.TextoDeEstatus
                               }).ToArray();
                return Json(new { data = Surtido }, JsonRequestBehavior.AllowGet);
            }
            return View();
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
            List<ClsProductos> Productos = new List<ClsProductos>();
            ClsParametros Parametro = (from q in ClsParametros.getList() where q.NombreDeParametro == "ActivaLike" select q).ToList().FirstOrDefault();
            if (Parametro != null)
            {
                if (Parametro.ValorDeParametro == "SI")
                {
                    Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras.ToUpper().Contains(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.CodigoDeBarras }).ToList();
                    if (Productos.Count == 0)
                    {
                        Productos = (from q in ClsProductos.getList() where q.NombreDeProducto.ToUpper().Contains(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.NombreDeProducto }).ToList();
                    }
                }
                else
                {
                    Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras.ToUpper().StartsWith(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.CodigoDeBarras }).ToList();
                    if (Productos.Count == 0)
                    {
                        Productos = (from q in ClsProductos.getList() where q.NombreDeProducto.ToUpper().StartsWith(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.NombreDeProducto }).ToList();
                    }
                }
            }
            else
            {
                Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras.ToUpper().StartsWith(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.CodigoDeBarras }).ToList();
                if (Productos.Count == 0)
                {
                    Productos = (from q in ClsProductos.getList() where q.NombreDeProducto.ToUpper().StartsWith(Prefix.ToUpper()) select new ClsProductos() { NombreDeProducto = q.NombreDeProducto }).ToList();
                }
            }
            return Json(Productos, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsProductos Producto = new ClsProductos();
            Producto = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).FirstOrDefault();
            if (Producto == null)
            {
                Producto = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).FirstOrDefault();
            }
            if (Producto == null)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar el producto";
            }
            return Json(new { Resultado, Producto }, JsonRequestBehavior.AllowGet);
        }
        private void CargaCombos(SurtidoModel Entidad)
        {
            ViewBag.CveDeAplicaSurtido = ClsAdicional.ClsCargaCombo.CargaComboClave(4,Entidad.CveDeAplicaSurtido.ToString());
            ViewBag.CveDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveDeEstatus.ToString());
        }
    }
}