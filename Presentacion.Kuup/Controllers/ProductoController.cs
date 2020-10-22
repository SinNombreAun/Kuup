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

namespace Presentacion.Kuup.Controllers
{
    public class ProductoController : BaseController
    {
        ClsOperaProducto Opera = new ClsOperaProducto();
        #region Index
        [HttpGet]
        public ActionResult Index(bool Grid = false)
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            if (Grid)
            {
                var Producto = (from q in ClsProductos.getList()
                                select new
                                {
                                    q.NumeroDeProducto,
                                    q.CodigoDeBarras,
                                    q.NombreDeProducto,
                                    q.CantidadDeProductoTotal,
                                    q.PrecioUnitario,
                                    q.TextoAviso,
                                    q.TextoCorreoSurtido,
                                    q.TextoAplicaMayoreo,
                                    q.CantidadMinimaMayoreo,
                                    q.PrecioMayoreo,
                                    q.TextoEstatus
                                }).ToArray();
                if (Producto.Length != 0)
                {
                    return Json(new { data = Producto }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { data = Producto }, JsonRequestBehavior.AllowGet);
                }
            }
            return View();
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
                RegistroCapturado.fCveEstatus = (byte)ClsEnumerables.CveEstatusGeneral.ACTIVO;

                Resultado = Opera.Insert(RegistroCapturado, (fGeneraCodigoDeBarras == 2));
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
            ViewBag.Resultado = Resultado.MensajeController();
            return View(RegistroCapturado);
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
            ProductoModel Producto = new ProductoModel((from q in ClsProductos.getList() where q.NumeroDeProducto == NumeroDeProducto select q).ToList().FirstOrDefault());
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
            this.CargaCombos(Producto);
            return View(Producto);
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
            ClsAdicional.ClsResultado ResultadoGeneral = new ClsAdicional.ClsResultado(true, String.Empty);
            String NombreDeArchivo = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(strinjson))
                {
                    List<ProductoModel> Productos = ClsAdicional.Deserializar<List<ProductoModel>>(strinjson);
                    if (Productos.Count > 0)
                    {
                        List<String> Mensajes = new List<String>();
                        foreach (var Producto in Productos)
                        {
                            if (Producto.CveAviso == 2)
                            {
                                Producto.CveCorreoSurtido = 2;
                            }
                            Producto.FechaDeRegistro = DateTime.Now;
                            Producto.CantidadDeProductoNueva = Producto.CantidadDeProductoTotal;
                            Producto.CveEstatus = (byte)ClsEnumerables.CveEstatusGeneral.ACTIVO;
                            ClsAdicional.ClsResultado Resultado = Opera.Insert(Producto, (String.IsNullOrEmpty(Producto.CodigoDeBarras)));
                            if (!Resultado.Resultado)
                            {
                                Mensajes.Add("El producto con Codigo de Barras: " + Producto.CodigoDeBarras + " Nombre de producto: " + Producto.NombreDeProducto + " no puedo ser insertado debido a " + Resultado.Mensaje);
                            }
                        }
                        if (Mensajes.Count > 0)
                        {
                            ResultadoGeneral.Resultado = false;
                            ResultadoGeneral.Mensaje = "Ocurrio un error al dar de alta algun registro de producto";
                            NombreDeArchivo = "AltaDeProductos" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                            ClsAdicional.ClsArchivos.EscribeArchivo(Server.MapPath(ClsConfiguracion.RutaDescarga) + NombreDeArchivo, String.Join(Environment.NewLine, Mensajes));
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
            ViewBag.CveAviso = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveAviso.ToString());
            ViewBag.CveCorreoSurtido = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveCorreoSurtido.ToString());
            ViewBag.NumeroDeProveedor = ClsAdicional.ClsCargaCombo.CargaComboProveedor(Entidad.NumeroDeProveedor);
            ViewBag.CveAplicaMayoreo = ClsAdicional.ClsCargaCombo.CargaComboClave(4, Entidad.CveAplicaMayoreo.ToString());
            ViewBag.CveEstatus = ClsAdicional.ClsCargaCombo.CargaComboClave(1, Entidad.CveEstatus.ToString());

            ViewBag.ManejaProveedor = ((from q in ClsParametros.getList() where q.NombreDeParametro == "ManejaProveedor" select q.ValorDeParametro).FirstOrDefault() == "SI");

        }
        #endregion
    }
}
