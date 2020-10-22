using Funciones.Kuup.Adicionales;
using Funciones.Kuup.Nodo;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class HomeController : BaseController
    {
        #region Index
        [HttpGet]
        public ActionResult Index()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            return View();
        }
        #endregion
        #region MenuBarra
        public ActionResult MenuBar()
        {
            if (!ValidaSesion())
            {
                return RedirectToAction("LoginOut", "Account");
            }
            ClsNodo estructuraMenu = this.ConsultaArbol();
            ViewData["lstMenu"] = estructuraMenu.Menus("");
            ViewBag.NombreDeUsuario = String.Format("{0} {1} {2}", MoSesion.NombreDePersona, MoSesion.ApellidoPaterno, MoSesion.ApellidoMaterno).ToUpper();
            return PartialView("MenuBar");
        }
        public ClsNodo ConsultaArbol()
        {
            List<ClsPantallasPerfil> LstPantallaPerfil = (from q in ClsPantallasPerfil.getList() where q.NumeroDePerfil == MoSesion.NumeroDePerfil select q).ToList();
            List<ClsMenu> LstMenu = (from q in ClsMenu.getList() orderby q.NumeroDeMenuPadre ascending, q.NumeroDeOrden ascending, q.NumeroDePantalla ascending select q).ToList();
            LstMenu.RemoveAll(f => (!LstPantallaPerfil.Exists(x => x.NumeroDePantalla == f.NumeroDePantalla)) && f.NumeroDeMenuPadre != 0);
            Dictionary<String, object> Informacion = new Dictionary<String, object>();
            Informacion.Add("Arbol de Nodos para Menú", "");
            ClsNodo NodoRaiz = new ClsNodo(0, Informacion);
            if (LstMenu.Count != 0)
            {
                foreach (var item in LstMenu)
                {
                    Informacion = new Dictionary<String, object>();
                    Informacion.Add("NombreInterno", item.NombreDePantallaInt);
                    Informacion.Add("NumeroDeMenu", item.NumeroDeMenu);
                    Informacion.Add("NombreDeMenu", item.NombreDeMenu);
                    Informacion.Add("NumeroDePantalla", item.NumeroDePantalla);
                    Informacion.Add("NombreDePantalla", item.NombreDePantalla);
                    Informacion.Add("NombreInternoDePantalla", item.NombreDePantallaInt);
                    Informacion.Add("NumeroDePadre", item.NumeroDeMenuPadre);

                    NodoRaiz.IntegraNodo(item.NumeroDeMenuPadre, new ClsNodo(item.NumeroDeMenu, Informacion));
                }
            }
            return NodoRaiz;
        }
        #endregion
        #region Agenda
        public JsonResult CargaEventos()
        {
            List<ClsAgenda> Eventos = (from q in ClsAgenda.getList() where q.NumeroDeUsuario == MoSesion.NumeroDeUsuario select q).ToList();
            return Json(new { eventos = Eventos }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AltaAgenda(AgendaModel RegistroCapturado)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            RegistroCapturado.fFechaDeAlta = System.DateTime.Now;
            RegistroCapturado.fNumeroDeUsuario = MoSesion.NumeroDeUsuario;
            RegistroCapturado.fCveDeEstatus = (byte)ClsEnumerables.CveEstatusGeneral.ACTIVO;
            if (ModelState.IsValid)
            {
                if (RegistroCapturado.Insert())
                {
                    Resultado.Adicional = RegistroCapturado.NumeroDeAgenda;
                    Resultado.Resultado = true;
                    Resultado.Mensaje = "Registro de Alta correcto.";
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Registro de Alta incorrecto.";
                }
            }
            else
            {
                Resultado.Resultado = false;
                Resultado.Mensaje = "Ocurrio un error de datos.";
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAgenda()
        {
            String JsonText = String.Empty;
            foreach (var Evento in ClsAgenda.getList())
            {
                if (JsonText != String.Empty)
                {
                    JsonText += ",{";
                    JsonText += "\"id\":\"" + Evento.NumeroDeAgenda + "\",";
                    JsonText += "\"title\":\"" + Evento.Descripcion + "\",";
                    JsonText += "\"start\":\"" + Evento.FechaDeInicioDeEvento.ToString("yyyy-MM-dd HH:mm") + "\",";
                    if (Evento.FechaDeInicioDeEvento == Evento.FechaDeFinDeEvento)
                    {
                        JsonText += "\"allDay\":true";
                    }
                    else
                    {
                        JsonText += "\"end\":\"" + Evento.FechaDeFinDeEvento.ToString("yyyy-MM-dd HH:mm") + "\"";
                    }
                    JsonText += "}";
                }
                else
                {
                    JsonText += "{";
                    JsonText += "\"id\":\"" + Evento.NumeroDeAgenda + "\",";
                    JsonText += "\"title\":\"" + Evento.Descripcion + "\",";
                    JsonText += "\"start\":\"" + Evento.FechaDeInicioDeEvento.ToString("yyyy-MM-dd HH:mm") + "\",";
                    if (Evento.FechaDeInicioDeEvento == Evento.FechaDeFinDeEvento)
                    {
                        JsonText += "\"allDay\":true";
                    }
                    else
                    {
                        JsonText += "\"end\":\"" + Evento.FechaDeFinDeEvento.ToString("yyyy-MM-dd HH:mm") + "\"";
                    }
                    JsonText += "}";
                }
            }
            if (JsonText == String.Empty)
            {
                JsonText = "[{}]";
            }
            else
            {
                JsonText = "[ " + JsonText + "]";
            }
            return Json(JsonText, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EliminaAgenda(short NumeroDeAgenda)
        {
            ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado();
            if ((from q in ClsAgenda.getList() where q.NumeroDeAgenda == NumeroDeAgenda select q).Count() != 0)
            {
                ClsAgenda Agenda = (from q in ClsAgenda.getList() where q.NumeroDeAgenda == NumeroDeAgenda select q).FirstOrDefault();
                if (Agenda.Delete())
                {
                    Resultado.Resultado = true;
                    Resultado.Mensaje = "Registro de Agenda eliminado de forma correcta";
                }
                else
                {
                    Resultado.Resultado = false;
                    Resultado.Mensaje = "Registro de Agenda eliminado de forma incorrecta";
                }
            }
            return Json(Resultado, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}