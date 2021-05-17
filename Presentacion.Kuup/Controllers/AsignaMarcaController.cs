using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Funciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class AsignaMarcaController : BaseController
    {
        readonly short NumeroDePantalla = (new ClsAsignaMarcas()).NumeroDePantallaKuup;
        public ActionResult Index(String Origen = "Tipo", String TipoDeProductoMarca = "",String ControllerOrigen = "")
        {
            ViewBag.Origen = Origen;
            if (!String.IsNullOrEmpty(TipoDeProductoMarca))
            {
                TipoDeProductoMarca = TipoDeProductoMarca.Split('/')[0].Trim();
                if (Origen == "Tipo")
                {
                    ClsTiposDeProductos TipoDeProducto = (from q in ClsTiposDeProductos.getList(String.Format("NumeroDeTipoDeProducto == {0}", TipoDeProductoMarca)) select q).FirstOrDefault();
                    ViewBag.TipoDeProductoMarca = TipoDeProducto.NumeroDeTipoDeProducto.ToString() + " / " + TipoDeProducto.NombreDeTipoDeProducto;
                }
                else
                {
                    ClsMarcas Marca = (from q in ClsMarcas.getList(String.Format("NumeroDeMarca == {0}", TipoDeProductoMarca)) select q).FirstOrDefault();
                    ViewBag.TipoDeProductoMarca = Marca.NumeroDeMarca.ToString() + " / " + Marca.NombreDeMarca;
                }
                ViewBag.ControllerOrigen = ControllerOrigen;
                ViewBag.NumeroDeElemento = short.Parse(TipoDeProductoMarca);
            }
            else
            {
                ViewBag.ControllerOrigen = String.Empty;
            }
            return View();
        }
        public JsonResult CargaAsignados(String TipoDeProductoMarca, String Origen)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!String.IsNullOrEmpty(TipoDeProductoMarca))
            {
                TipoDeProductoMarca = TipoDeProductoMarca.Split('/')[0].Trim();
                if (Origen == "Tipo")
                {
                    var TipoOMarca = (from q in ClsAsignaMarcas.getList("NumeroDeTipoDeProducto == " + TipoDeProductoMarca).ToList() select new { MarcaOTipo = q.NombreDeMarca, NumeroMarcaOTipo = q.NumeroDeMarca }).ToList();
                    if (TipoOMarca.Count() == 0)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "No cuenta con ninguna asignacion";
                    }
                    return Json(new { Resultado, data = TipoOMarca }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var TipoOMarca = (from q in ClsAsignaMarcas.getList("NumeroDeMarca == " + TipoDeProductoMarca).ToList() select new { MarcaOTipo = q.NombreDeTipoDeProducto, NumeroMarcaOTipo = q.NumeroDeTipoDeProducto }).ToList();
                    if (TipoOMarca.Count() == 0)
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "No cuenta con ninguna asignacion";
                    }
                    return Json(new { Resultado, data = TipoOMarca }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Filtro nulo";
            }
            return Json(new { Resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaNoAsignados(String TipoDeProductoMarca, String Origen)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!String.IsNullOrEmpty(TipoDeProductoMarca))
            {
                TipoDeProductoMarca = TipoDeProductoMarca.Split('/')[0].Trim();
                var TipoOMarca = (from q in ClsAsignaMarcas.NoAsignadosPorOrigen(short.Parse(TipoDeProductoMarca), Origen) select new { MarcaOTipo = q.NOMBRE, NumeroMarcaOTipo = q.NUMERO }).ToList();
                if (TipoOMarca.Count() == 0)
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No cuenta con ninguna asignacion";
                }
                return Json(new { Resultado, data = TipoOMarca }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Filtro nulo";
            }
            return Json(new { Resultado }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AgregaElemento(String Origen,short NumeroDeElementoPadre, short NumeroElemento)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true,String.Empty);
            if(Origen == "Tipo")
            {
                Resultado = (new ClsOperaMarca()).AsignaMarcaATipo(NumeroDeElementoPadre,NumeroElemento);
            }
            else
            {
                Resultado = (new ClsOperaTipoDeProducto()).AsignaTipoAMarca(NumeroDeElementoPadre,NumeroElemento);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemueveElemento(String Origen, short NumeroDeElementoPadre, short NumeroElemento)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (Origen == "Tipo")
            {
                Resultado = (new ClsOperaMarca()).RemueveMarcaDeTipo(NumeroDeElementoPadre, NumeroElemento);
            }
            else
            {
                Resultado = (new ClsOperaTipoDeProducto()).RemueveTipoDeMarca(NumeroDeElementoPadre, NumeroElemento);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AutoCompleteMarcaOTipProducto(String Prefix, String Origen)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteMarcaOTipProducto(Prefix, Origen), JsonRequestBehavior.AllowGet);
        }
        public JsonResult AsignaMarcaMasivo()
        {
            foreach(var TipoProducto in ClsTiposDeProductos.getList())
            {
                List<short?> ListaMarcas = (from q in ClsProductos.getList("NumeroDeTipoDeProducto == " + TipoProducto.NumeroDeTipoDeProducto.ToString()) where q.NumeroDeMarca != 0 select q.NumeroDeMarca).Distinct().ToList();
                foreach(var Marca in ListaMarcas)
                {
                    ClsAsignaMarcas AsignaMarcaAtipoProducto = new ClsAsignaMarcas()
                    {
                        NumeroDeTipoDeProducto = TipoProducto.NumeroDeTipoDeProducto,
                        NumeroDeMarca = (short)Marca
                    };
                    if (!AsignaMarcaAtipoProducto.Insert())
                    {
                        return Json(new { NumeroDeMarca = Marca }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new { Resultado = "Correcto" }, JsonRequestBehavior.AllowGet);
        }
    }
}
