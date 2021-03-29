using Funciones.Kuup.Adicionales;
using Mod.Entity;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Funciones;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class TipoDeProductoController : BaseController
    {
        readonly ClsOperaTipoDeProducto Opera = new ClsOperaTipoDeProducto();
        readonly short NumeroDePantalla = (new ClsTiposDeProductos()).NumeroDePantallaKuup;
        #region Index
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
            this.CargaCombosParaTabla();
            return View();
        }
        [HttpPost]
        public ActionResult Json()
        {
            return Json((new ClsTiposDeProductos()).DataTableTipoProducto(new Negocio.Kuup.Globales.ClsDataTables(Request.Form)), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Alta
        [HttpGet]
        public ActionResult Alta()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return RedirectToAction("Index", "TipoDeProducto");
            }
            TipoDeProductoModel Entidad = new TipoDeProductoModel();
            this.CargaCombos(Entidad);
            return View();
        }
        [HttpPost]
        public ActionResult Alta(TipoDeProductoModel RegistroCapturado, byte fAsignaMarcas)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return RedirectToAction("Index", "TipoDeProducto");
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (ModelState.IsValid)
            {
                RegistroCapturado.fCveDeEstatus = (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO;

                Resultado = Opera.Insert(ref RegistroCapturado);
                if (Resultado.Resultado)
                {
                    if (fAsignaMarcas == 1)
                    {
                        return RedirectToAction("Index", "AsignaMarca", new { Origen = "Tipo", TipoDeProductoMarca = RegistroCapturado.NumeroDeTipoDeProducto, ControllerOrigen = "TipoDeProducto" });
                    }
                    else if (fAsignaMarcas == 2)
                    {
                        return RedirectToAction("Detalle", "TipoDeProducto", new { NumeroDeElemento = RegistroCapturado.NumeroDeTipoDeProducto });
                    }
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Campos incorrectos";
            }
            this.CargaCombos(RegistroCapturado);
            TempData["Resultado"] = Resultado.MensajeController();
            return View(RegistroCapturado);
        }
        #endregion
        #region Edita
        [HttpGet]
        public ActionResult Edita(short NumeroDeTipoDeProducto)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "TipoDeProducto", new { NumeroDeTipoDeProducto });
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            var TipoDeProductoClase = ClsTiposDeProductos.getList(String.Format("NumeroDeTipoDeProducto == {0}", NumeroDeTipoDeProducto));
            TipoDeProductoModel TipoDeProducto = new TipoDeProductoModel(TipoDeProductoClase.FirstOrDefault());
            if (TipoDeProducto == null)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El Tipo de Producto no existe";
                TempData["Resultado"] = Resultado.MensajeController();
                return RedirectToAction("Index", "TipoDeProducto");
            }
            this.CargaCombos(TipoDeProducto);
            return View(TipoDeProducto);
        }
        [HttpPost]
        public ActionResult Edita(TipoDeProductoModel RegistroCapturado)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "TipoDeProducto", new { RegistroCapturado.fNumeroDeTipoDeProducto });
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Tipo de Producto actualizado de forma correcta");
            var TipoDeProductoClase = ClsTiposDeProductos.getList(String.Format("NumeroDeTipoDeProducto == {0}", RegistroCapturado.fNumeroDeTipoDeProducto));
            TipoDeProductoModel TipoDeProducto = new TipoDeProductoModel(TipoDeProductoClase.FirstOrDefault());
            if (ModelState.IsValid)
            {
                TipoDeProducto.NombreDeTipoDeProducto = RegistroCapturado.NombreDeTipoDeProducto;
                if (!TipoDeProducto.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un problema al actualizar el reigstro";
                }
                else
                {
                    ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                    ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "EDITA");
                    TipoDeProducto.InsertAudit(Audit);
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Campos incorrectos";
            }
            if (!Resultado.Resultado)
            {
                this.CargaCombos(RegistroCapturado);
                TempData["Resultado"] = Resultado.MensajeController();
                return View(RegistroCapturado);
            }
            return RedirectToAction("Detalle", "TipoDeProducto", new { NumeroDeElemento = RegistroCapturado.NumeroDeTipoDeProducto });
        }
        #endregion
        #region Baja
        public ActionResult Baja(short NumeroDeTipoDeProducto)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.BAJA))
            {
                return RedirectToAction("Index", "TipoDeProducto");
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            var TipoDeProductoClase = ClsTiposDeProductos.getList(String.Format("NumeroDeTipoDeProducto == {0} && CveDeEstatus == 1", NumeroDeTipoDeProducto));
            if (TipoDeProductoClase.Count() == 0)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El tipo de producto no se encuentra o no cuenta con el estatus correcto";
            }
            else
            {
                TipoDeProductoModel TipoDeProducto = new TipoDeProductoModel(TipoDeProductoClase.FirstOrDefault());
                TipoDeProducto.CveDeEstatus = 2;
                if (!TipoDeProducto.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible realizar la baja del tipo de producto";
                }
                else
                {
                    ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                    ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "BAJA");
                    TipoDeProducto.InsertAudit(Audit);
                    Resultado.Resultado = true;
                    Resultado.Mensaje = "Baja correcto";
                }
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Detalle
        [HttpGet]
        public ActionResult Detalle(short NumeroDeElemento)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.DETALLE))
            {
                return RedirectToAction("Index", "TipoDeProducto");
            }
            var TipoDeProductoClase = ClsTiposDeProductos.getList(String.Format("NumeroDeTipoDeProducto == {0}", NumeroDeElemento));
            TipoDeProductoModel TipoDeProducto = new TipoDeProductoModel(TipoDeProductoClase.FirstOrDefault());
            if (TipoDeProducto == null)
            {
                return RedirectToAction("Index", "TipoDeProducto");
            }
            else
            {
            }
            this.CargaCombos(TipoDeProducto);
            return View(TipoDeProducto);
        }
        #endregion
        #region Adicional
        private void CargaCombos(TipoDeProductoModel Entidad)
        {
            ViewBag.CveDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveDeEstatus.ToString());
        }
        private void CargaCombosParaTabla()
        {
            ViewBag.TextoDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClaveParaTabla(1, "TextoDeEstatus");
        }
        #endregion
    }
}
