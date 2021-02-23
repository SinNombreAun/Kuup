using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rotativa.MVC;
using System.IO;

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
            ViewBag.FechaInicialLimite = (from q in ClsVentasTotales.getList() select q.FechaDeOperacion).Min(x => x).ToString("yyyy-MM-dd");
            return View();
        }
        public JsonResult ObtenVentas(String fFechaInicial, String fFechaFinal)
        {
            String filtro = String.Empty;
            if (string.IsNullOrEmpty(fFechaInicial))
            {
                fFechaInicial = (from q in ClsVentasTotales.getList() select q.FechaDeOperacion).Min(x => x).ToString("yyyy-MM-dd");
            }
            DateTime DFechaInicial = Convert.ToDateTime(fFechaInicial).AddHours(00).AddMinutes(00).AddSeconds(00);
            if (String.IsNullOrEmpty(fFechaFinal))
            {
                fFechaFinal = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime DFechaFinal = Convert.ToDateTime(fFechaFinal).AddHours(23).AddMinutes(59).AddSeconds(59);
            filtro = String.Format("FechaDeOperacion >= DateTime({0},{1},{2},{3},{4},{5})", DFechaInicial.Year, DFechaInicial.Month, DFechaInicial.Day, DFechaInicial.Hour, DFechaInicial.Minute, DFechaInicial.Second);
            filtro += String.Format("&& FechaDeOperacion <= DateTime({0},{1},{2},{3},{4},{5})", DFechaFinal.Year, DFechaFinal.Month, DFechaFinal.Day, DFechaFinal.Hour, DFechaFinal.Minute, DFechaFinal.Second);
            var VentasTotales = (from q in ClsVentasTotales.getList(filtro)
                                 select new
                                 {
                                     q.FolioDeOperacion,
                                     FechaDeOperacion = q.FechaDeOperacion.ToString("yyyy-MM-dd hh:mm:ss tt"),
                                     q.NombreDeUsuario,
                                     q.ImporteEntregado,
                                     q.ImporteCambio,
                                     q.ImporteNeto,
                                     q.TextoDeEstatus
                                 }).OrderBy(x => x.FechaDeOperacion);
            return Json(new { data = VentasTotales }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenDetalleDeVentas(short fFolioDeOperacion)
        {
            String filtro = "FolioDeOperacion == " + fFolioDeOperacion.ToString();
            var DetalleDeVenta = (from q in ClsVentas.getList(filtro)
                                  select new {
                                      q.FolioDeOperacion,
                                      q.NombreDeProducto,
                                      q.CantidadDeProducto,
                                      q.PrecioUnitario,
                                      q.ImporteDeProducto
                                  });
            return Json(new { data = DetalleDeVenta }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReporteVenta(String fFechaInicial, String fFechaFinal, bool sessionValid = false)
        {
            if (!sessionValid)
            {
                if (!ValidaSesion())
                {
                    return RedirectToAction("LoginOut", "Account");
                }
                if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.REPORTE))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            if (string.IsNullOrEmpty(fFechaInicial))
            {
                fFechaInicial = (from q in ClsVentasTotales.getList() select q.FechaDeOperacion).Min(x => x).ToString("yyyy-MM-dd");
            }
            DateTime DFechaInicial = Convert.ToDateTime(fFechaInicial).AddHours(00).AddMinutes(00).AddSeconds(00);
            if (String.IsNullOrEmpty(fFechaFinal))
            {
                fFechaFinal = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime DFechaFinal = Convert.ToDateTime(fFechaFinal).AddHours(23).AddMinutes(59).AddSeconds(59);
            ViewData["FechaInicio"] = DFechaInicial.ToString("yyyy-MM-dd");
            ViewData["FechaFin"] = DFechaFinal.ToString("yyyy-MM-dd");
            List<Mod.Entity.VentasTotalesDetalle_Result> VentasTotales = ClsVentas.VentaDetalle(DFechaInicial,DFechaFinal,0);
            ViewData["Web"] = true;
            if (sessionValid)
            {
                ViewData["Web"] = false;
            }
            return View(VentasTotales);
        }
        public ActionResult GeneraReporte(String fFechaInicial, String fFechaFinal,String filtro)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.REPORTE))
            {
                return RedirectToAction("Index", "Venta");
            }
            String Ruta = Server.MapPath(ClsConfiguracion.RutaDescarga + "/" + MoSesion.NombreDeUsuario + "/");
            if (ClsAdicional.ClsArchivos.FileExists(Ruta, true))
            {
                ClsAdicional.ClsArchivos.CrearDirectorio(Ruta);
            }
            bool sessionValid = true;
            ActionAsPdf Pdf =  new ActionAsPdf("ReporteVenta", new { fFechaInicial, fFechaFinal, sessionValid });
            Pdf.FileName = String.Format("VentaTotal-{0}{1}{2}.pdf",DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            Pdf.RotativaOptions.PageSize = Rotativa.Core.Options.Size.A4;
            Pdf.RotativaOptions.PageOrientation = Rotativa.Core.Options.Orientation.Portrait;
            byte[] byteArray = Pdf.BuildPdf(ControllerContext);
            FileStream FileStream = new FileStream(Ruta + Pdf.FileName, FileMode.Create, FileAccess.Write);
            FileStream.Write(byteArray, 0, byteArray.Length);
            FileStream.Close();
            return File(Ruta + Pdf.FileName, System.Net.Mime.MediaTypeNames.Application.Octet, Pdf.FileName);
        }
    }
}
