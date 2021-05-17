using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
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
        List<DateTime> Fechas = new List<DateTime>();
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
            Fechas = (from q in ClsVentasTotales.getList() select q.FechaDeOperacion).ToList();
            if(Fechas.Count() != 0)
            {
                ViewBag.FechaInicialLimite = Fechas.Min(x => x).ToString("yyyy-MM-dd");
                ViewBag.MuestraBotones = true;
            }
            else
            {
                ViewBag.FechaInicialLimite = DateTime.Now.ToString("yyyy-MM-dd");
                ViewBag.MuestraBotones = false;
            }
            return View();
        }
        public JsonResult ObtenVentas(String fFechaInicial, String fFechaFinal)
        {
            String filtro = String.Empty;
            if (string.IsNullOrEmpty(fFechaInicial))
            {
                if (Fechas.Count() != 0)
                {
                    fFechaInicial = Fechas.Min(x => x).ToString("yyyy-MM-dd");
                }
                else
                {
                    fFechaInicial = DateTime.Now.ToString("yyyy-MM-dd");
                }
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
        public ActionResult ReporteVenta(String fFechaInicial, String fFechaFinal,String Tipo)
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
            if (!ClsAdicional.ClsArchivos.FileExists(Ruta, true))
            {
                ClsAdicional.ClsArchivos.CrearDirectorio(Ruta);
            }
            if (string.IsNullOrEmpty(fFechaInicial))
            {
                if (Fechas.Count() != 0)
                {
                    fFechaInicial = Fechas.Min(x => x).ToString("yyyy-MM-dd");
                }
                else
                {
                    fFechaInicial = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            DateTime DFechaInicial = Convert.ToDateTime(fFechaInicial).AddHours(00).AddMinutes(00).AddSeconds(00);
            if (String.IsNullOrEmpty(fFechaFinal))
            {
                fFechaFinal = DateTime.Now.ToString("yyyy-MM-dd");
            }
            DateTime DFechaFinal = Convert.ToDateTime(fFechaFinal).AddHours(23).AddMinutes(59).AddSeconds(59);

            TempDataDictionary temporalData = new TempDataDictionary
            {
                { "FechaInicio", DFechaInicial.ToString("yyyy-MM-dd") },
                { "FechaFin", DFechaFinal.ToString("yyyy-MM-dd") },
                { "Web", (Tipo == "Web") }
            };

            List<Mod.Entity.VentasTotalesDetalle_Result> VentasTotales = ClsVentas.VentaDetalle(DFechaInicial, DFechaFinal, 0);
            String FileName = String.Format("VentaTotal-{0}{1}{2}",DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            PDFBuild<List<Mod.Entity.VentasTotalesDetalle_Result>> pDFBuild = new PDFBuild<List<Mod.Entity.VentasTotalesDetalle_Result>>(ControllerContext, "ReporteVenta", FileName, Ruta, temporalData, VentasTotales);
            if(Tipo != "Web")
            {
                return pDFBuild.generaPDF();
            }
            foreach(var tempdata in temporalData)
            {
                TempData[tempdata.Key] = tempdata.Value;
            }
            return View(VentasTotales);
        }
    }
}
