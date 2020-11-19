using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class VentaController : BaseController
    {
        //readonly ClsOperaVenta Opera = new ClsOperaVenta();
        readonly short NumeroDePantalla = (new ClsVentas()).NumeroDePantallaKuup;
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
        public JsonResult ObtenVentas(String fFechaInicial, String fFechaFinal)
        {
            DateTime DFechaInicial = Convert.ToDateTime(fFechaInicial);
            DateTime DFechaFinal = Convert.ToDateTime(fFechaFinal);
            var VentasTotales = (from q in ClsVentasTotales.getList()
                                 where q.FechaDeOperacion >= DFechaInicial && q.FechaDeOperacion <= DFechaFinal
                                 select new
                                 {
                                     q.FolioDeOperacion,
                                     q.FechaDeOperacion,
                                     q.NombreDeUsuario,
                                     q.ImporteEntregado,
                                     q.ImporteCambio,
                                     q.ImporteNeto,
                                     q.TextoDeEstatus
                                 });
            return Json(new { data = VentasTotales }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenDetalleDeVentas(short FolioDeOperacion)
        {
            var DetalleDeVenta = (from q in ClsVentas.getList()
                                  where q.FolioDeOperacion == FolioDeOperacion
                                  select new {
                                      q.FolioDeOperacion,
                                      q.NombreDeProducto,
                                      q.CantidadDeProducto,
                                      q.PrecioUnitario,
                                      q.ImporteDeProducto
                                  });
            return Json(new { data = DetalleDeVenta }, JsonRequestBehavior.AllowGet);
        }
    }
}
