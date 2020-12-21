﻿using Funciones.Kuup.Adicionales;
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
            ViewBag.FechaInicialLimite = (from q in ClsVentasTotales.getList() select q.FechaDeOperacion).Min(x => x).ToString("yyyy-MM-dd");
            return View();
        }
        public JsonResult ObtenVentas(String fFechaInicial, String fFechaFinal)
        {
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
            var VentasTotales = (from q in ClsVentasTotales.getList()
                                 where q.FechaDeOperacion >= DFechaInicial && q.FechaDeOperacion <= DFechaFinal
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
            var DetalleDeVenta = (from q in ClsVentas.getList()
                                  where q.FolioDeOperacion == fFolioDeOperacion
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