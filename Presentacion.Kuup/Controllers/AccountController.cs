using Funciones.Kuup.Adicionales;
using Funciones.Kuup.Cifrado;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            MoSesion.LimpiaSesion();
            AccountModel Account = new AccountModel();
            return View(Account);
        }
        [HttpPost]
        public ActionResult Login(AccountModel Account)
        {
            if (ModelState.IsValid)
            {
                Account.IP = ClsAdicional.ClsGetHostNameAndIP.GetIPv4Address(Request.UserHostAddress);
                Account.Terminal = ClsAdicional.ClsGetHostNameAndIP.GetHostName(Request.UserHostAddress); 
                ViewData["Informacion"] = string.Empty;
                List<ClsUsuarios> lstUsuario = (from q in ClsUsuarios.getList() where q.NombreDeUsuario == Account.NombreDeUsuario && MoCifrado.Descifrado(q.PasswordUsuario) == Account.Password && q.CveDeEstatus == 1 select q).ToList();
                if (lstUsuario.Count == 1)
                {
                    List<ClsIPRegistradas> lstIPRegistrada = (from q in ClsIPRegistradas.getList() where q.NumeroDeUsuario == lstUsuario.FirstOrDefault().NumeroDeUsuario && q.Terminal.ToUpper() == Account.Terminal.ToUpper() && q.IP == Account.IP select q).ToList();
                    if (lstIPRegistrada.Count == 1)
                    {
                        List<ClsUsuariosPerfil> lstUsuarioPerfil = (from q in ClsUsuariosPerfil.getList() where q.NumeroDeUsuario == lstUsuario.FirstOrDefault().NumeroDeUsuario select q).ToList();
                        if (lstUsuarioPerfil.Count == 1)
                        {
                            MoSesion.NumeroDeUsuario = lstUsuario.FirstOrDefault().NumeroDeUsuario;
                            MoSesion.NombreDeUsuario = lstUsuario.FirstOrDefault().NombreDeUsuario;
                            MoSesion.IPDeUsuario = lstIPRegistrada.FirstOrDefault().IP;
                            MoSesion.TerminalDeUsuario = lstIPRegistrada.FirstOrDefault().Terminal;
                            MoSesion.CveTipoDeAcceso = lstIPRegistrada.FirstOrDefault().CveTipoDeAcceso;
                            MoSesion.TextoTipoDeAccedo = lstIPRegistrada.FirstOrDefault().TextoTipoDeAccedo;
                            MoSesion.NumeroDePerfil = lstUsuarioPerfil.FirstOrDefault().NumeroDePerfil;
                            MoSesion.NombreDePerfil = lstUsuarioPerfil.FirstOrDefault().NombreDePerfil;
                            MoSesion.Browser = Request.Browser.Browser + " " + Request.Browser.Version;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewData["Informacion"] = "El usuario no tiene aun asignado ningún perfil";
                        }
                    }
                    else
                    {
                        ViewData["Informacion"] = "El equipo " + Account.Terminal + " e IP " + Account.IP + " no se encuentran registrados para iniciar sesión en Kuup";
                    }
                }
                else
                {
                    ViewData["Informacion"] = "El usuario y contraseña son incorrectos, favor de verificar";
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            MoSesion.LimpiaSesion();
            return RedirectToAction("Login", "Account");
        }
    }
}