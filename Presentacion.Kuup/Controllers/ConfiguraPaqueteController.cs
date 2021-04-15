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
            return Json((new ClsConfiguraPaquetes()).DataTableConfiguraPaquete(new Negocio.Kuup.Globales.ClsDataTables(Request.Form)), JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Detalle(short NumeroDeProductoPadre, short NumeroDeProductoHijo)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ACCESO))
            {
                return RedirectToAction("Index", "Home");
            }
            var ConfiguraPaquete = ClsConfiguraPaquetes.getList(String.Format("NumeroDeProductoPadre == {0} && NumeroDeProductoHijo == {1}", NumeroDeProductoPadre, NumeroDeProductoHijo));
            if(ConfiguraPaquete.Count() == 0)
            {
                return RedirectToAction("Index", "ConfiguraPaquete");
            }
            ConfiguraPaqueteModel Configura = new ConfiguraPaqueteModel(ConfiguraPaquete.FirstOrDefault());
            this.CargaCombos(Configura);
            return View(Configura);
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
            this.CargaCombos();
            return View();
        }
        [HttpPost]
        public JsonResult Edita(short NumeroDeProductoPadre, String CodigoDeBarrasPadre, decimal PrecioDeProductoPadre, short NumeroDeProductoHijo, String CodigoDeBarrasHijo, decimal PrecioDeProductoHijo, byte CantidadASalir)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsConfiguraPaquetes configuraPaquetes = new ClsConfiguraPaquetes()
            {
                NumeroDeProductoPadre = NumeroDeProductoPadre,
                CodigoDeBarrasPadre = CodigoDeBarrasPadre,
                PrecioDeProductoPadre = PrecioDeProductoPadre,
                NumeroDeProductoHijo = NumeroDeProductoHijo,
                CodigoDeBarrasHijo = CodigoDeBarrasHijo,
                PrecioDeProductoHijo = PrecioDeProductoHijo,
                CantidadASalir = CantidadASalir,
                ImporteTotal = Math.Round((PrecioDeProductoPadre + PrecioDeProductoHijo),2)
            };
            if (configuraPaquetes.Existe())
            {
                if (configuraPaquetes.Update())
                {
                    Resultado.Adicional = "";
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible registrar los cambios en la configuración";
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "La configuración a guardar no existe en el sistema";
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GuardaConfiguracion(short NumeroDeProductoPadre,String CodigoDeBarrasPadre, decimal PrecioDeProductoPadre, short NumeroDeProductoHijo, String CodigoDeBarrasHijo, decimal PrecioDeProductoHijo,byte CantidadASalir) {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ClsConfiguraPaquetes configuraPaquetes = new ClsConfiguraPaquetes()
            {
                NumeroDeProductoPadre = NumeroDeProductoPadre,
                CodigoDeBarrasPadre = CodigoDeBarrasPadre,
                PrecioDeProductoPadre = PrecioDeProductoPadre,
                NumeroDeProductoHijo = NumeroDeProductoHijo,
                CodigoDeBarrasHijo = CodigoDeBarrasHijo,
                PrecioDeProductoHijo = PrecioDeProductoHijo,
                CantidadASalir = CantidadASalir,
                ImporteTotal = Math.Round((PrecioDeProductoPadre + PrecioDeProductoHijo), 2)
            };
            if (!configuraPaquetes.Existe())
            {
                if (configuraPaquetes.Insert())
                {
                    Resultado.Adicional = "";
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible registrar la configuración de este paquete";
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "La configuración a guardar ya existe dentro del sistema";
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UrlBuscaPrecioDeProducto(String CodigoONombreDeProducto)
        {
            String Filtro = String.Format("NombreDeProducto == \"{0}\" && CveDeEstatus == 1", CodigoONombreDeProducto);
            var Encontrado = ClsProductos.getList(Filtro);
            if (Encontrado.Count() == 0)
            {
                Filtro = String.Format("CodigoDeBarras == \"{0}\" && CveDeEstatus == 1", CodigoONombreDeProducto);
                Encontrado = ClsProductos.getList(Filtro);
            }
            return Json(new { NumeroDeProducto = Encontrado.FirstOrDefault().NumeroDeProducto, NombreDeProducto = Encontrado.FirstOrDefault().NombreDeProducto, CodigoDeBarras = Encontrado.FirstOrDefault().CodigoDeBarras, PrecioDeProducto = Encontrado.FirstOrDefault().PrecioUnitario }, JsonRequestBehavior.AllowGet);
        }
        public void CargaCombos(ConfiguraPaqueteModel Configura = null)
        {
            if(Configura == null)
            {
                Configura = new ConfiguraPaqueteModel();
            }
            ViewBag.NumeroDeTipoDeProducto = ClsAdicional.ClsCargaCombo.CargaComboTipoDeProducto(String.Empty);
            ViewBag.NumeroDeMarca = ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(0, String.Empty);
        }
        public JsonResult AutoCompleteProducto(String Prefix, short NumeroDeTipoDeProducto = 0, short NumeroDeMarca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix, NumeroDeTipoDeProducto, NumeroDeMarca), JsonRequestBehavior.AllowGet);
        }
        public JsonResult ObtenMarcaPorTipo(short TipoDeProducto, short Marca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(TipoDeProducto, Marca.ToString()), JsonRequestBehavior.AllowGet);
        }
    }
}
