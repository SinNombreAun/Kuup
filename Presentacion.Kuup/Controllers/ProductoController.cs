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
using Mod.Entity;

namespace Presentacion.Kuup.Controllers
{
    public class ProductoController : BaseController
    {
        readonly ClsOperaProducto Opera = new ClsOperaProducto();
        readonly short NumeroDePantalla = (new ClsProductos()).NumeroDePantallaKuup;
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
            return Json((new ClsProductos()).DataTableProducto(new Negocio.Kuup.Globales.ClsDataTables(Request.Form)), JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("Index", "Producto");
            }
            ProductoModel Entidad = new ProductoModel();
            this.CargaCombos(Entidad);
            return View();
        }
        [HttpPost]
        public ActionResult Alta(ProductoModel RegistroCapturado, byte fGeneraCodigoDeBarras)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return RedirectToAction("Index", "Producto");
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (fGeneraCodigoDeBarras == 2)
            {
                ModelState.Remove("fCodigoDeBarras");
                RegistroCapturado.fCodigoDeBarras = "-";
            }
            if (RegistroCapturado.fCveAviso == 2)
            {
                ModelState.Remove("fCveCorreoSurtido");
                RegistroCapturado.fCveCorreoSurtido = 2;
                ModelState.Remove("fCantidadMinima");
                RegistroCapturado.fCantidadMinima = 0;
                ModelState.Remove("fNumeroDeProveedor");
                RegistroCapturado.fNumeroDeProveedor = 0;
            }
            if (ModelState.IsValid)
            {
                RegistroCapturado.fFechaDeRegistro = DateTime.Now;
                RegistroCapturado.fCantidadDeProductoNueva = RegistroCapturado.CantidadDeProductoTotal;
                RegistroCapturado.fCveDeEstatus = (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO;

                Resultado = Opera.Insert(RegistroCapturado, (fGeneraCodigoDeBarras == 2), new List<MayoreoProducto>());
                if (Resultado.Resultado)
                {
                    return RedirectToAction("Detalle", "Producto", new { RegistroCapturado.NumeroDeProducto });
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
        public ActionResult Edita(short NumeroDeProducto, String CodigoDeBarras)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "Producto", new { NumeroDeProducto });
            }
            String Filtro = String.Empty;
            Filtro = String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\"", NumeroDeProducto, CodigoDeBarras);
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            ProductoModel productos = (from q in ClsProductos.getList(Filtro)
                                       select new ProductoModel()
                                       {
                                           NumeroDeProducto = q.NumeroDeProducto,
                                           CodigoDeBarras = q.CodigoDeBarras,
                                           FechaDeRegistro = q.FechaDeRegistro,
                                           CantidadDeProductoUltima = q.CantidadDeProductoUltima,
                                           CantidadDeProductoNueva = q.CantidadDeProductoNueva,
                                           CantidadDeProductoTotal = q.CantidadDeProductoTotal,
                                           NombreDeProducto = q.NombreDeProducto,
                                           Descripcion = q.Descripcion,
                                           NumeroDeTipoDeProducto = q.NumeroDeTipoDeProducto,
                                           NumeroDeMarca = q.NumeroDeMarca,
                                           CveAviso = q.CveAviso,
                                           CveCorreoSurtido = q.CveCorreoSurtido,
                                           CantidadMinima = q.CantidadMinima,
                                           NumeroDeProveedor = q.NumeroDeProveedor,
                                           PrecioUnitario = q.PrecioUnitario,
                                           CveDeEstatus = q.CveDeEstatus,
                                           NombreDeTipoDeProducto = q.NombreDeTipoDeProducto,
                                           NombreDeMarca = q.NombreDeMarca,
                                           TextoAviso = q.TextoAviso,
                                           TextoCorreoSurtido = q.TextoCorreoSurtido,
                                           NombreDeProveedor = q.NombreDeProveedor,
                                           TextoDeEstatus = q.TextoDeEstatus
                                       }).FirstOrDefault();
            if (productos == null)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El producto no existe";
                TempData["Resultado"] = Resultado.MensajeController();
                return RedirectToAction("Index", "Producto");
            }
            this.CargaCombos(productos);
            return View(productos);
        }
        [HttpPost]
        public ActionResult Edita(ProductoModel RegistroCapturado)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.EDITA))
            {
                return RedirectToAction("Detalle", "Producto", new { RegistroCapturado.fNumeroDeProducto });
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Producto actualizado de forma correcta");
            String Filtro = String.Empty;
            Filtro = String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\"", RegistroCapturado.NumeroDeProducto, RegistroCapturado.CodigoDeBarras);
            ProductoModel productos = (ProductoModel)(from q in ClsProductos.getList(Filtro)
                                                      select new ProductoModel()
                                                      {
                                                          NumeroDeProducto = q.NumeroDeProducto,
                                                          CodigoDeBarras = q.CodigoDeBarras,
                                                          FechaDeRegistro = q.FechaDeRegistro,
                                                          CantidadDeProductoUltima = q.CantidadDeProductoUltima,
                                                          CantidadDeProductoNueva = q.CantidadDeProductoNueva,
                                                          CantidadDeProductoTotal = q.CantidadDeProductoTotal,
                                                          NombreDeProducto = q.NombreDeProducto,
                                                          Descripcion = q.Descripcion,
                                                          NumeroDeTipoDeProducto = q.NumeroDeTipoDeProducto,
                                                          NumeroDeMarca =q.NumeroDeMarca,
                                                          CveAviso = q.CveAviso,
                                                          CveCorreoSurtido = q.CveCorreoSurtido,
                                                          CantidadMinima = q.CantidadMinima,
                                                          NumeroDeProveedor = q.NumeroDeProveedor,
                                                          PrecioUnitario = q.PrecioUnitario,
                                                          CveDeEstatus = q.CveDeEstatus,
                                                          NombreDeTipoDeProducto = q.NombreDeTipoDeProducto,
                                                          NombreDeMarca = q.NombreDeMarca,
                                                          TextoAviso = q.TextoAviso,
                                                          TextoCorreoSurtido = q.TextoCorreoSurtido,
                                                          NombreDeProveedor = q.NombreDeProveedor,
                                                          TextoDeEstatus = q.TextoDeEstatus
                                                      }).FirstOrDefault();
            if (RegistroCapturado.fCveAviso == 2)
            {
                ModelState.Remove("fCveCorreoSurtido");
                RegistroCapturado.fCveCorreoSurtido = 2;
                ModelState.Remove("fCantidadMinima");
                RegistroCapturado.fCantidadMinima = 0;
                ModelState.Remove("fNumeroDeProveedor");
                RegistroCapturado.fNumeroDeProveedor = 0;
            }
            if (ModelState.IsValid)
            {
                productos.NombreDeProducto = RegistroCapturado.NombreDeProducto;
                productos.Descripcion = RegistroCapturado.Descripcion;
                productos.NumeroDeTipoDeProducto = RegistroCapturado.NumeroDeTipoDeProducto;
                productos.NumeroDeMarca = RegistroCapturado.NumeroDeMarca;
                productos.PrecioUnitario = RegistroCapturado.PrecioUnitario;
                productos.CveAviso = RegistroCapturado.CveAviso;
                productos.CveCorreoSurtido = RegistroCapturado.CveCorreoSurtido;
                productos.CantidadMinima = RegistroCapturado.CantidadMinima;
                if (!productos.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un problema al actualizar el reigstro";
                }
                else
                {
                    ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                    ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "EDITA");
                    productos.InsertAudit(Audit);
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
            return RedirectToAction("Detalle", "Producto", new { RegistroCapturado.NumeroDeProducto });
        }
        #endregion
        #region Baja
        [HttpPost]
        public ActionResult Baja(short NumeroDeProducto, String CodigoDeBarras)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut","Account");
            }
            if(!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.BAJA))
            {
                return RedirectToAction("Index", "Producto");
            }
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            var ProductoClase = ClsProductos.getList(String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\"", NumeroDeProducto, CodigoDeBarras));
            ProductoModel Producto = new ProductoModel(ProductoClase.FirstOrDefault());
            if (Producto == null)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "El producto no se encuentra o no cuenta con el estatus correcto";
            }
            else
            {
                List<ClsConfiguraMayoreos> Mayoreos = ClsConfiguraMayoreos.getList(String.Format("NumeroDeProducto == {0} && CodigoDeBarras == \"{1}\"", Producto.NumeroDeProducto, Producto.CodigoDeBarras));
                List<ClsConfiguraPaquetes> Paquetes = ClsConfiguraPaquetes.getList(String.Format("(NumeroDeProductoPadre == {0} && CodigoDeBarrasPadre == \"{1}\") || (NumeroDeProductoHijo == {0} && CodigoDeBarrasHijo == \"{1}\")", Producto.NumeroDeProducto, Producto.CodigoDeBarras));
                using (DBKuupEntities db = new DBKuupEntities())
                {
                    using (var Transaccion = db.Database.BeginTransaction())
                    {
                        try
                        {
                            if(Mayoreos.Count() != 0)
                            {
                                foreach(var item in Mayoreos)
                                {
                                    if (!item.Delete())
                                    {
                                        Transaccion.Rollback();
                                        Resultado.Resultado = false;
                                        Resultado.Mensaje = "No fue posible realizar la baja de la configuracion de mayoreo asignada a este producto";
                                        return Json(Resultado, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            if(Paquetes.Count() != 0)
                            {
                                foreach(var item in Paquetes)
                                {
                                    if (!item.Delete())
                                    {
                                        Transaccion.Rollback();
                                        Resultado.Resultado = false;
                                        Resultado.Mensaje = "No fue posible realizar la baja de los paquetes asignados al producto";
                                        return Json(Resultado, JsonRequestBehavior.AllowGet);
                                    }
                                }
                            }
                            Producto.CveDeEstatus = 2;
                            if (!Producto.Update())
                            {
                                Resultado.Resultado = false;
                                Resultado.Mensaje = "No fue posible realizar la baja del producto";
                                Transaccion.Rollback();
                            }
                            else
                            {
                                ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                                ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "BAJA");
                                Producto.InsertAudit(Audit);
                                Resultado.Resultado = true;
                                Resultado.Mensaje = "Baja correcto";
                                Transaccion.Commit();
                            }
                        }
                        catch (Exception e) 
                        {
                            Transaccion.Rollback();
                            Resultado.Resultado = false;
                            Resultado.Mensaje = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                            ClsBitacora.GeneraBitacora(NumeroDePantalla, 4, "Baja", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                        }
                    } 
                }
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Detalle
        [HttpGet]
        public ActionResult Detalle(short NumeroDeProducto)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.DETALLE))
            {
                return RedirectToAction("Index", "Producto");
            }
            var ProductoClase = ClsProductos.getList(String.Format("NumeroDeProducto == {0}", NumeroDeProducto));
            ProductoModel Producto = new ProductoModel(ProductoClase.FirstOrDefault());
            if (Producto == null)
            {
                return RedirectToAction("Index", "Producto");
            }
            ViewBag.RutaDeCodigoDeBarras = "";
            ClsCodigosDeBarras CodigosDeBarras = (from q in ClsCodigosDeBarras.getList() where q.CodigoDeBarras == Producto.CodigoDeBarras && q.NumeroDeProducto == Producto.NumeroDeProducto select q).ToList().FirstOrDefault();
            if (CodigosDeBarras != null)
            {
                ViewBag.RutaDeCodigoDeBarras = CodigosDeBarras.RutaDeArchivo;
            }
            ViewBag.AplicarPrecioDeMayoreo = "SI";
            if (ClsConfiguraMayoreos.getList().Exists(x => x.NumeroDeProducto == Producto.NumeroDeProducto && x.CodigoDeBarras == Producto.CodigoDeBarras))
            {
                ViewBag.AplicarPrecioDeMayoreo = "NO";
            }
            this.CargaCombos(Producto);
            return View(Producto);
        }
        public JsonResult CargaMayoreo(short NumeroDeProducto, String CodigoDeBarras)
        {
            var configuraMayoreos = (from q in ClsConfiguraMayoreos.getList() where q.NumeroDeProducto == NumeroDeProducto && q.CodigoDeBarras == CodigoDeBarras select new { q.NumeroDeMayoreo, q.CantidadMinima, q.CantidadMaxima, q.PrecioDeMayoreo }).ToArray();
            return Json(new { data = configuraMayoreos }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GuardaMayoreo(short NumeroDeProducto, String CodigoDeBarras, String Mayoreos)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            if (!ValidaSesion())
            {
                //return RedirectToAction("LoginOut", "Account");
            }
            try
            {
                if (!String.IsNullOrEmpty(Mayoreos))
                {
                    List<ClsConfiguraMayoreos> ConfiguraMayoreos = ClsAdicional.Deserializar<List<ClsConfiguraMayoreos>>(Mayoreos);
                    if (ConfiguraMayoreos.Count() > 0)
                    {
                        Resultado = Opera.RegistraMayoreo(NumeroDeProducto, CodigoDeBarras, ConfiguraMayoreos);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Importar
        [HttpGet]
        public ActionResult Importar()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.IMPORTAR))
            {
                return RedirectToAction("Index", "Producto");
            }
            ViewBag.TitulosCampos = Opera.ListaDeDatosAImportar();
            return View();
        }
        #endregion
        #region AltaMasiva
        [HttpPost]
        public ActionResult AltaMasiva(String strinjson)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.IMPORTAR))
            {
                return RedirectToAction("Index", "Producto");
            }
            ClsAdicional.ClsResultado ResultadoGeneral = new ClsAdicional.ClsResultado(true, String.Empty);
            String NombreDeArchivo = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(strinjson))
                {
                    List<ProductoModel> Productos = ClsAdicional.Deserializar<List<ProductoModel>>(strinjson);
                    List<MayoreoProducto> Mayoreo = ClsAdicional.Deserializar<List<MayoreoProducto>>(strinjson);
                    if (Productos.Count > 0)
                    {
                        List<String> Mensajes = new List<String>();
                        String Previo = String.Empty;
                        foreach (var Producto in Productos.Distinct())
                        {
                            if (Previo != Producto.CodigoDeBarras + Producto.NombreDeProducto)
                            {
                                Previo = Producto.CodigoDeBarras + Producto.NombreDeProducto;
                                if (Producto.CveAviso == 2)
                                {
                                    Producto.CveCorreoSurtido = 2;
                                }
                                Producto.FechaDeRegistro = DateTime.Now;
                                Producto.CantidadDeProductoNueva = Producto.CantidadDeProductoTotal;
                                Producto.CveDeEstatus = (byte)ClsEnumerables.CveDeEstatusGeneral.ACTIVO;
                                ClsAdicional.ClsResultado Resultado = Opera.Insert(Producto, (String.IsNullOrEmpty(Producto.CodigoDeBarras)), Mayoreo);
                                if (!Resultado.Resultado)
                                {
                                    Mensajes.Add("El producto con Codigo de Barras: " + Producto.CodigoDeBarras + " Nombre de producto: " + Producto.NombreDeProducto + " no puedo ser insertado debido a " + Resultado.Mensaje);
                                }
                            }
                        }
                        if (Mensajes.Count > 0)
                        {
                            ResultadoGeneral.Resultado = false;
                            ResultadoGeneral.Mensaje = "Ocurrio un error al dar de alta algun registro de producto";
                            NombreDeArchivo = "AltaDeProductos" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            ClsAdicional.ClsArchivos.EscribeArchivo(Server.MapPath(ClsConfiguracion.RutaDescarga + "/" + MoSesion.NombreDeUsuario + "/") + NombreDeArchivo, String.Join(Environment.NewLine, Mensajes));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ResultadoGeneral.Resultado = false;
                ResultadoGeneral.Mensaje = "Ocurrio un error al dar de alta algun registro de producto, consultar bitacora";
            }
            return Json(new { Resultado = ResultadoGeneral, NombreDeArchivo = (String.IsNullOrEmpty(NombreDeArchivo) ? String.Empty : NombreDeArchivo.Split('.')[0]), Extencion = "txt" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ActualizaPrecios
        [HttpGet]
        public ActionResult ActualizaPrecios()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, 10))
            {
                return RedirectToAction("Index", "Producto");
            }
            return View();
        }
        public JsonResult UrlPreciosActualizados(String Precios)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Precios Actualizados");
            if (!ValidaSesion())
            {
                return Json(new { UrlAccount = Url.Action("LoginOut", "Account") }, JsonRequestBehavior.AllowGet);
            }
            if (!ValidaFuncionalidad(NumeroDePantalla, (byte)ClsEnumerables.Funcionalidades.ALTA))
            {
                return Json(new { UrlFun = Url.Action("Index", "VentaTotal") }, JsonRequestBehavior.AllowGet);
            }
            List<String> Datos = Precios.Split(':').ToList();
            foreach (var Dato in Datos)
            {
                List<String> Elementos = new List<string>();
                if (Dato.Contains("PrecioUnitario"))
                {
                    Elementos = Dato.Split('&').ToList();
                    ClsProductos Producto = (from q in ClsProductos.getList()
                                             where q.NumeroDeProducto == ClsAdicional.Convert<short>(Elementos[0].Split('=')[1])
                                             select q).FirstOrDefault();
                    if (Producto != null)
                    {
                        Producto.PrecioUnitario = ClsAdicional.Convert<decimal>(Elementos[1].Split('=')[1]);
                    }
                    if (!Producto.Update())
                    {
                        Resultado.Resultado = false;
                        Resultado.Mensaje = "Ocurrio un error al actualizar el precio unitario del Producto";
                        break;
                    }
                    else
                    {
                        ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                        ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ACTUALIZAPRECIO");
                        Producto.InsertAudit(Audit);
                    }
                }
                else if (Dato.Contains("NumeroDeMayoreo"))
                {
                    Elementos = Dato.Split('&').ToList();
                    ClsConfiguraMayoreos ConfiguraMayoreo = (from q in ClsConfiguraMayoreos.getList()
                                                             where q.NumeroDeProducto == ClsAdicional.Convert<short>(Elementos[0].Split('=')[1]) &&
                                                             q.NumeroDeMayoreo == ClsAdicional.Convert<short>(Elementos[1].Split('=')[1])
                                                             select q).FirstOrDefault();
                    if (ConfiguraMayoreo != null)
                    {
                        ConfiguraMayoreo.PrecioDeMayoreo = ClsAdicional.Convert<decimal>(Elementos[2].Split('=')[1]);
                        if (!ConfiguraMayoreo.Update())
                        {
                            Resultado.Resultado = false;
                            Resultado.Mensaje = "Ocurrio un error al actualizar el precio de mayoreo";
                            break;
                        }
                        else
                        {
                            ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                            ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ACTUALIZAPRECIO");
                            ConfiguraMayoreo.InsertAudit(Audit);
                        }
                    }

                }
                else if (Dato.Contains("PrecioDeProductoPadre") || Dato.Contains("PrecioDeProductoHijo"))
                {
                    Elementos = Dato.Split('&').ToList();
                    ClsConfiguraPaquetes ConfiguraPaquetes = (from q in ClsConfiguraPaquetes.getList()
                                                              where q.NumeroDeProductoPadre == ClsAdicional.Convert<short>(Elementos[0].Split('=')[1]) &&
                                                              q.NumeroDeProductoHijo == ClsAdicional.Convert<short>(Elementos[1].Split('=')[1])
                                                              select q).FirstOrDefault();
                    if (ConfiguraPaquetes != null)
                    {
                        if (Dato.Contains("PrecioDeProductoPadre"))
                        {
                            ConfiguraPaquetes.PrecioDeProductoPadre = ClsAdicional.Convert<decimal>(Elementos[2].Split('=')[1]);
                        }
                        else if (Dato.Contains("PrecioDeProductoHijo"))
                        {
                            ConfiguraPaquetes.PrecioDeProductoHijo = ClsAdicional.Convert<decimal>(Elementos[2].Split('=')[1]);
                        }
                        if (!ConfiguraPaquetes.Update())
                        {
                            Resultado.Resultado = false;
                            Resultado.Mensaje = "Ocurrio un error al actualizar el precio de paquetes";
                            break;
                        }
                        else
                        {
                            ClsSequence Sequence = new ClsSequence((new DBKuupEntities()).Database);
                            ClsAudit Audit = Nucleo.Clases.ClsAuditInsert.RegistraAudit(Sequence.SQ_FolioAudit(), "ACTUALIZAPRECIO");
                            ConfiguraPaquetes.InsertAudit(Audit);
                        }
                    }
                }

            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CargaTodosLosPrecios(String NombreOCodigoDeProducto)
        {
            var Productos = (from q in ClsProductos.getList() where q.CodigoDeBarras == NombreOCodigoDeProducto select q).FirstOrDefault();
            if (Productos == null)
            {
                Productos = (from q in ClsProductos.getList() where q.NombreDeProducto == NombreOCodigoDeProducto select q).FirstOrDefault();
            }
            List<Object> Precios = new List<object>();
            if (Productos != null)
            {
                Precios.Add(new { id = Productos.NumeroDeProducto, Tipo = "Precio Unitario", Campo = "Precio Unitario", Valor = Productos.PrecioUnitario });

                var PreciosMayoreo = (from q in ClsConfiguraMayoreos.getList() where q.NumeroDeProducto == Productos.NumeroDeProducto && q.CodigoDeBarras == Productos.CodigoDeBarras orderby q.NumeroDeMayoreo select q).ToList();
                foreach (var Mayoreo in PreciosMayoreo)
                {
                    Precios.Add(new { id = Mayoreo.NumeroDeMayoreo, Tipo = "Precio Mayoreo", Campo = String.Format("Precio Mayoreo de {0} a {1}", Mayoreo.CantidadMinima, (Mayoreo.CantidadMaxima == null ? "sin limite" : Mayoreo.CantidadMaxima.ToString())), Valor = Mayoreo.PrecioDeMayoreo });
                }
                var ConfiguraPaqueteP = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoPadre == Productos.NumeroDeProducto select q).ToList();
                foreach (var ConfPaqueteP in ConfiguraPaqueteP)
                {
                    Precios.Add(new { id = ConfPaqueteP.NumeroDeProductoPadre.ToString() + "_" + ConfPaqueteP.NumeroDeProductoHijo.ToString(), Tipo = "Precio Paquete Padre", Campo = "Precio de Producto Padre", Valor = ConfPaqueteP.PrecioDeProductoPadre, CampoHijo = "Producto Hijo " + ConfPaqueteP.NombreDeProductoHijo, ValorHijo = ConfPaqueteP.PrecioDeProductoHijo });
                }
                var ConfiguraPaqueteH = (from q in ClsConfiguraPaquetes.getList() where q.NumeroDeProductoHijo == Productos.NumeroDeProducto select q).ToList();
                foreach (var ConfPaqueteH in ConfiguraPaqueteH)
                {
                    Precios.Add(new { id = ConfPaqueteH.NumeroDeProductoPadre.ToString() + "_" + ConfPaqueteH.NumeroDeProductoHijo.ToString(), Tipo = "Precio Paquete Hijo", Campo = "Precio de Producto Hijo", Valor = ConfPaqueteH.PrecioDeProductoHijo, CampoPadre = "Producto Padre " + ConfPaqueteH.NombreDeProductoPadre, ValorPadre = ConfPaqueteH.PrecioDeProductoPadre });
                }
            }

            return Json(new { Precios }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Adicional
        public ActionResult DescargaArchivo()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            String Ruta = Server.MapPath(ClsConfiguracion.RutaDescarga + "/" + MoSesion.NombreDeUsuario + "/");
            if (ClsAdicional.ClsArchivos.FileExists(Ruta, true, true))
            {
                if (ClsAdicional.ClsArchivos.FileExists(Ruta + "ArchivoDeCarga.csv", false))
                {
                    ClsAdicional.ClsArchivos.EliminaArchivo(Ruta + "ArchivoDeCarga.csv");
                }
                else
                {
                    ClsAdicional.ClsArchivos.CrearArchivo(Ruta + "ArchivoDeCarga.csv");
                }
                ClsAdicional.ClsArchivos.EscribreArchivoCSV(Ruta + "ArchivoDeCarga.csv",
                    Opera.ListaDeDatosAImportar(),
                    Opera.DatosDeEjemplo());
            }
            return File(Ruta + "ArchivoDeCarga.csv", System.Net.Mime.MediaTypeNames.Application.Octet, "ArchivoDeCarga.csv");
        }
        public ContentResult SubirArchivo(GeneralModel.FileUploadModelCsv RegistroCapturado)
        {
            String Resultado = "<div id='Validador'>";
            if (Path.GetExtension(RegistroCapturado.Archivo.FileName) == ".csv")
            {
                if (RegistroCapturado.Archivo != null && RegistroCapturado.Archivo.ContentLength > 0)
                {
                    try
                    {
                        if (ClsAdicional.ClsArchivos.FileExists(Server.MapPath(ClsConfiguracion.RutaUpload), true, true))
                        {
                            RegistroCapturado.Archivo.SaveAs(Path.Combine(Server.MapPath(ClsConfiguracion.RutaUpload), Path.GetFileName(RegistroCapturado.Archivo.FileName)));
                            Resultado += String.Empty + "</div>";
                        }
                    }
                    catch (Exception e)
                    {
                        Resultado += "Ocurrio un error inesperado" + "</div>";
                    }
                }
                else
                {
                    Resultado += "El registro del archivo vien vacio" + "</div>";
                }
            }
            else
            {
                Resultado += "El archivo a subir no cuenta con la extencion correcta CSV" + "</div>";
            }
            ViewBag.TitulosCampos = Opera.ListaDeDatosAImportar();
            return Content(Resultado);
        }
        public JsonResult Validar(String NombreDelArchivo)
        {
            String RutaDelArchivo = Server.MapPath(ClsConfiguracion.RutaUpload) + NombreDelArchivo;
            IEnumerable<Object> productos = new List<ClsProductos>();
            ClsAdicional.ClsResultado resultado = Opera.ImportarCSVProducto(RutaDelArchivo, ref productos);
            return Json(new { data = new { data = productos.ToArray() }, Resultado = resultado }, JsonRequestBehavior.AllowGet);
        }
        private void CargaCombos(ProductoModel Entidad)
        {
            ViewBag.NumeroDeTipoDeProducto = ClsAdicional.ClsCargaCombo.CargaComboTipoDeProducto(Entidad.NumeroDeTipoDeProducto.ToString());
            ViewBag.NumeroDeMarca = ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(Entidad.NumeroDeTipoDeProducto,Entidad.NumeroDeMarca.ToString());
            ViewBag.CveAviso = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveAviso.ToString());
            ViewBag.CveCorreoSurtido = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveCorreoSurtido.ToString());
            ViewBag.NumeroDeProveedor = ClsAdicional.ClsCargaCombo.CargaComboProveedor(Entidad.NumeroDeProveedor);
            ViewBag.CveDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveDeEstatus.ToString());

            ViewBag.ManejaProveedor = ((from q in ClsParametros.getList() where q.NombreDeParametro == "ManejaProveedor" select q.ValorDeParametro).FirstOrDefault() == "SI");
        }
        private void CargaCombosParaTabla()
        {
            ViewBag.NombreDeTipoDeProducto = ClsAdicional.ClsCargaCombo.CargaComboTipoDeProductoParaTabla("NombreDeTipoDeProducto");
            ViewBag.TextoAviso = ClsAdicional.ClsCargaCombo.CargaComboClaveParaTabla(4, "TextoAviso");
            ViewBag.TextoCorreoSurtido = ClsAdicional.ClsCargaCombo.CargaComboClaveParaTabla(4, "TextoCorreoSurtido");
            ViewBag.TextoDeEstatus = ClsAdicional.ClsCargaCombo.CargaComboClaveParaTabla(1, "TextoDeEstatus");
        }
        public JsonResult AutoCompleteProducto(String Prefix)
        {
            return Json(ClsAdicional.ClsCargaCombo.AutoCompleteProducto(Prefix), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CodigosDeBarras()
        {
            List<CodigoDeBarrasModel> CodigoDeBarras = (from q in ClsCodigosDeBarras.getList() select new CodigoDeBarrasModel {
                fNumeroDeProducto = q.NumeroDeProducto,
                fNombreDeProducto = q.NombreDeProducto,
                fCodigoDeBarras = q.CodigoDeBarras,
                fRutaDeArchivo = q.RutaDeArchivo
            }).OrderBy(x => x.NumeroDeProducto).ToList();
            return View(CodigoDeBarras);
        }
        public JsonResult ObtenMarcaPorTipo(short TipoDeProducto, short Marca = 0)
        {
            return Json(ClsAdicional.ClsCargaCombo.CargaComboMarcaPorTipo(TipoDeProducto, Marca.ToString()), JsonRequestBehavior.AllowGet);
        }
        //public ActionResult GeneraPDFCodigoDeBarras()
        //{
        //    return new ViewAsPdf("CodigosDeBarras") { FileName = "CodigosDeBarras.pdf"};
        //}
        #endregion
    }
}
