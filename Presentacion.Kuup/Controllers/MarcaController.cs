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
    public class MarcaController : BaseController
    {
        readonly ClsOperaMarca Opera = new ClsOperaMarca();
        readonly short NumeroDePantalla = (new ClsMarcas()).NumeroDePantallaKuup;
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
            return Json((new ClsMarcas()).DataTableMarca(new Negocio.Kuup.Globales.ClsDataTables(Request.Form)), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Index", "Marca");
            }
            MarcaModel Entidad = new MarcaModel();
            this.CargaCombos(Entidad);
            return View();
        }
        [HttpPost]
        public ActionResult Alta(MarcaModel RegistroCapturado, byte fAsignaMarcas)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return RedirectToAction("Index", "Marca");
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
                        return RedirectToAction("Index", "AsignaMarca", new { Origen = "Marca", TipoDeProductoMarca = RegistroCapturado.NumeroDeMarca, ControllerOrigen = "Marca" });
                    }
                    else if(fAsignaMarcas == 2)
                    {
                        return RedirectToAction("Detalle", "Marca", new { NumeroDeElemento = RegistroCapturado.NumeroDeMarca });
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
        public ActionResult Edita(short NumeroDeMarca)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "Marca", new { NumeroDeMarca });
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            var MarcaClase = ClsMarcas.getList(String.Format("NumeroDeMarca == {0}", NumeroDeMarca));
            MarcaModel Marca = new MarcaModel(MarcaClase.FirstOrDefault());
            if (Marca == null)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El marca no existe";
                TempData["Resultado"] = Resultado.MensajeController();
                return RedirectToAction("Index", "Marca");
            }
            this.CargaCombos(Marca);
            return View(Marca);
        }
        [HttpPost]
        public ActionResult Edita(MarcaModel RegistroCapturado)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "Marca", new { RegistroCapturado.fNumeroDeMarca });
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Marca actualizada de forma correcta");
            var MarcaClase = ClsMarcas.getList(String.Format("NumeroDeMarca == {0}", RegistroCapturado.fNumeroDeMarca));
            MarcaModel Marca = new MarcaModel(MarcaClase.FirstOrDefault());
            if (ModelState.IsValid)
            {
                Marca.NombreDeMarca = RegistroCapturado.NombreDeMarca;
                if (!Marca.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un problema al actualizar el reigstro";
                }
                else
                {
                    ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                    ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "EDITA");
                    Marca.InsertAudit(Audit);
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
            return RedirectToAction("Detalle", "Marca", new { NumeroDeElemento = RegistroCapturado.NumeroDeMarca });
        }
        #endregion
        #region Baja
        public ActionResult Baja(short NumeroDeMarca)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.BAJA))
            {
                return RedirectToAction("Index", "Marca");
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            var MarcaClase = ClsMarcas.getList(String.Format("NumeroDeMarca == {0} && CveDeEstatus == 1", NumeroDeMarca));
            if(MarcaClase.Count() == 0)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "La marca no se encuentra o no cuenta con el estatus correcto";
            }
            else
            {
                MarcaModel Marca = new MarcaModel(MarcaClase.FirstOrDefault());
                Marca.CveDeEstatus = 2;
                if (!Marca.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible realizar la baja de la Marca";
                }
                else
                {
                    ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                    ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "BAJA");
                    Marca.InsertAudit(Audit);
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
                return RedirectToAction("Index", "Marca");
            }
            var MarcaClase = ClsMarcas.getList(String.Format("NumeroDeMarca == {0}", NumeroDeElemento));
            MarcaModel Marca = new MarcaModel(MarcaClase.FirstOrDefault());
            if (Marca == null)
            {
                return RedirectToAction("Index", "Marca");
            }
            else
            {
            }
            this.CargaCombos(Marca);
            return View(Marca);
        }
        #endregion
        #region Adicional
        private void CargaCombos(MarcaModel Entidad)
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
