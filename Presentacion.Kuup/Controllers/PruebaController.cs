using Funciones.Kuup.Adicionales;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Presentacion.Kuup.Controllers
{
    public class PruebaController : BaseController
    {
        // GET: Prueba
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult CreaArchivoEnTicket()
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, "Proceso correcto");
            try
            {
                if (!ClsAdicional.ClsArchivos.FileExists(Server.MapPath(ClsConfiguracion.Tickets) + "Prueba.txt", false, true))
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "No fue posible crear el archivo de prueba";
                }
                else
                {
                    ClsAdicional.ClsArchivos.EliminaArchivo(Server.MapPath(ClsConfiguracion.Tickets) + "Prueba.txt");
                }
            }
            catch(Exception e)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString());
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImprimeTicketDePrueba()
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            try
            {
                Funciones.Kuup.Tecket.ClsTicket Ticket = new Funciones.Kuup.Tecket.ClsTicket();
                Ticket.TextoCentro("Ticket de prueba");
                Funciones.Kuup.Tecket.ClsTicket.LineasCaracter("*");
                Ticket.TextoCentro("Hola! soy el sistema Kuup");
                Ticket.TextoCentro("Conmigo puedes realizar tus ventas con Ticket");
                Ticket.TextoIzquierda(String.Format("Fecha: {0} Hora: {1}", DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                Funciones.Kuup.Tecket.ClsTicket.LineasCaracter("*");

                Ticket.NombreDeTicket = String.Format("Ticket{0}{1}.txt", "Prueba", DateTime.Now.ToString("yyyyMMdd"));
                Ticket.RutaDeTicket = System.Web.HttpContext.Current.Server.MapPath(ClsConfiguracion.Tickets);

                Ticket.ImprimirTiket("POS-58", ref Resultado);
            }
            catch (Exception e)
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString());
            }

            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ListaImpresoras()
        {
            List<String> Impresoras = new List<string>();
            for (int i = 0; i < System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count; i++)
            {
                Impresoras.Add(System.Drawing.Printing.PrinterSettings.InstalledPrinters[i]);
            }
            return Json(new { Impresoras = string.Join(",",Impresoras)},JsonRequestBehavior.AllowGet);
        }
        public JsonResult CorrigeLetraEne(String A,String R)
        {
            // Ã± , ¥
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            foreach (var producto in ClsProductos.getList().Where(x => x.NombreDeProducto.Contains(A)))
            {
                producto.NombreDeProducto = producto.NombreDeProducto.Replace(A, R);
                if (!producto.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un error al corregir "+ R;
                    return Json(Resultado,JsonRequestBehavior.AllowGet);
                }
            }
            foreach (var producto in ClsProductos.getList().Where(x => x.Descripcion.Contains(A)))
            {
                producto.Descripcion = producto.Descripcion.Replace(A, R);
                if (!producto.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un error al corregir " + R;
                    return Json(Resultado, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ParseaTodoAMayusculas()
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            foreach (var Producto in ClsProductos.getList())
            {
                Producto.NombreDeProducto = Producto.NombreDeProducto.Trim().ToUpper();
                Producto.Descripcion = Producto.Descripcion.Trim().ToUpper();
                if (!Producto.Update())
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Ocurrio un error al hacer ToUpperCase";
                }
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreaImagneCodigoDeBarras()
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
            foreach (var Producto in ClsProductos.getList().Where(x => x.CodigoDeBarras.Contains("Kuup")).ToList().OrderBy(x => x.NumeroDeProducto))
            {
                Funciones.Kuup.CodigoDeBarras.MoCodigoDeBarras ResultadoCodigo = (new Funciones.Kuup.CodigoDeBarras.MoCodigoDeBarras()).GeneraCodigoDeBarras(Producto.NumeroDeProducto, "Kuup", Producto.CodigoDeBarras);
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
    }
}