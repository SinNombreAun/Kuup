using System;
using System.Web.Mvc;
using Funciones.Kuup.Adicionales;
using Presentacion.Kuup.Nucleo.Motores;

namespace Presentacion.Kuup.Controllers
{
    public class GeneralController : BaseController
    {
        public ActionResult SubirArchivo(String Accion, String Controlador, String UrlAccion, String BotonCargaDatos)
        {
            ViewBag.Accion = Accion;
            ViewBag.Controlador = Controlador;
            ViewBag.UrlAccion = UrlAccion;
            ViewBag.BotonCargaDatos = BotonCargaDatos;
            return View();
        }
        public ActionResult DescargaArchivo(String NombreDeArchivo, String Extencion, bool AgregarFecha = false, bool AgregarHora = false)
        {
            String Ruta = Server.MapPath(ClsConfiguracion.RutaDescarga + "/" + MoSesion.NombreDeUsuario + "/");
            if (AgregarFecha)
            {
                NombreDeArchivo += DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (AgregarHora)
            {
                NombreDeArchivo += DateTime.Now.ToString("HH-mm-ss");
            }
            return File(Ruta + NombreDeArchivo +  "." + Extencion, System.Net.Mime.MediaTypeNames.Application.Octet, NombreDeArchivo + "." + Extencion);
        }
        [AllowAnonymous]
        public ActionResult TituloReportes()
        {
            return View();
        }
    }
}