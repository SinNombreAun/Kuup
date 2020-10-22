using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.XPath;

namespace Presentacion.Kuup.Controllers
{
    public class VentaTotalController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            return View();
        }
        public JsonResult CargaProducto(String NombreOCodigoDeProducto)
        {
            List<ClsProductos> Productos = new List<ClsProductos>();
            ClsProductos Producto = new ClsProductos();
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).ToList();
            if (Productos.Count == 0)
            {
                Productos = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).ToList();
            }

            if (Productos.Count != 0)
            {
                Producto = Productos.FirstOrDefault();
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "No fue posible encontrar el producto a registrar";
            }
            return Json(new { Resultado, Producto }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteProducto(String Prefix)
        {
            List<ClsProductos> Productos = new List<ClsProductos>();
            Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras.StartsWith(Prefix) select new ClsProductos() { NombreDeProducto = q.CodigoDeBarras }).ToList();
            if (Productos.Count == 0)
            {
                Productos = (from q in ClsProductos.getList() where q.NombreDeProducto.StartsWith(Prefix) select new ClsProductos() { NombreDeProducto = q.NombreDeProducto }).ToList();
            }
            return Json(Productos, JsonRequestBehavior.AllowGet);
        }
    }
}