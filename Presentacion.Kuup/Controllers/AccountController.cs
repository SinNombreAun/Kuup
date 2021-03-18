using Funciones.Kuup.Adicionales;
using Funciones.Kuup.Cifrado;
using Negocio.Kuup.Clases;
using Presentacion.Kuup.Models;
using Presentacion.Kuup.Nucleo.Motores;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web.Mvc;

namespace Presentacion.Kuup.Controllers
{
    public class AccountController : Controller
    {
        private readonly short NumeroDePantalla = 0;
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
            try
            {
                if (ModelState.IsValid)
                {
                    Account.IP = ClsAdicional.ClsGetHostNameAndIP.GetIPv4Address(Request.UserHostAddress);
                    Account.Terminal = ClsAdicional.ClsGetHostNameAndIP.GetHostName(Request.UserHostAddress);
                    var eer = MoCifrado.Cifrado(Account.Password);
                    ViewData["Informacion"] = String.Empty;
                    List<ClsUsuarios> lstUsuario = (from q in ClsUsuarios.getList() where q.NombreDeUsuario == Account.NombreDeUsuario && MoCifrado.Descifrado(q.PasswordUsuario) == Account.Password && q.CveDeEstatus == 1 select q).ToList();
                    if (lstUsuario.Count == 1)
                    {
                        List<ClsParametros> Parametro = (from q in ClsParametros.getList() where q.NombreDeParametro == "ValidaIP" select q).ToList();
                        if (Parametro.Count() == 1)
                        {
                            List<ClsIPRegistradas> lstIPRegistrada = (from q in ClsIPRegistradas.getList() where q.NumeroDeUsuario == lstUsuario.FirstOrDefault().NumeroDeUsuario && q.Terminal.ToUpper() == Account.Terminal.ToUpper() && q.IP == Account.IP select q).ToList();
                            if(Parametro.FirstOrDefault().ValorDeParametro == "SI")
                            {
                                if (lstIPRegistrada.Count == 1)
                                {
                                    List<ClsUsuariosPerfil> lstUsuarioPerfil = (from q in ClsUsuariosPerfil.getList() where q.NumeroDeUsuario == lstUsuario.FirstOrDefault().NumeroDeUsuario select q).ToList();
                                    if (lstUsuarioPerfil.Count == 1)
                                    {
                                        MoSesion.NumeroDeUsuario = lstUsuario.FirstOrDefault().NumeroDeUsuario;
                                        MoSesion.NombreDeUsuario = lstUsuario.FirstOrDefault().NombreDeUsuario;
                                        MoSesion.NombreDePersona = lstUsuario.FirstOrDefault().NombreDePersona;
                                        MoSesion.ApellidoPaterno = lstUsuario.FirstOrDefault().ApellidoPaterno;
                                        MoSesion.ApellidoMaterno = lstUsuario.FirstOrDefault().ApellidoMaterno;
                                        MoSesion.IPDeUsuario = lstIPRegistrada.FirstOrDefault().IP;
                                        MoSesion.TerminalDeUsuario = lstIPRegistrada.FirstOrDefault().Terminal;
                                        MoSesion.CveTipoDeAcceso = lstIPRegistrada.FirstOrDefault().CveTipoDeAcceso;
                                        MoSesion.TextoTipoDeAccedo = lstIPRegistrada.FirstOrDefault().TextoTipoDeAccedo;
                                        MoSesion.NumeroDePerfil = lstUsuarioPerfil.FirstOrDefault().NumeroDePerfil;
                                        MoSesion.NombreDePerfil = lstUsuarioPerfil.FirstOrDefault().NombreDePerfil;
                                        MoSesion.Browser = Request.Browser.Browser + " " + Request.Browser.Version;
                                        if (!MvcApplication.ListaDeSesionesActivas.ContainsKey(MoSesion.IDSesion))
                                        {
                                            MvcApplication.ListaDeSesionesActivas.Add(MoSesion.IDSesion, new SesionActiva() { NumeroDeUsuario = MoSesion.NumeroDeUsuario, NumeroDePerfil = MoSesion.NumeroDePerfil, Deslogueado = false });
                                        }
                                        return RedirectToAction("Index", "Home");
                                    }
                                    else
                                    {
                                        ViewData["Informacion"] = Recursos.Textos.Account_ValidaPerfil;
                                    }
                                }
                                else
                                {
                                    ViewData["Informacion"] = String.Format(Recursos.Textos.Account_ValidaEquipo, Account.Terminal, Account.IP);
                                }
                            }
                            else
                            {
                                lstIPRegistrada = new List<ClsIPRegistradas>();
                                ClsIPRegistradas Ips = new ClsIPRegistradas();
                                Ips.IP = Account.IP;
                                Ips.Terminal = Account.Terminal;
                                Ips.CveTipoDeAcceso = 1;
                                List<ClsUsuariosPerfil> lstUsuarioPerfil = (from q in ClsUsuariosPerfil.getList() where q.NumeroDeUsuario == lstUsuario.FirstOrDefault().NumeroDeUsuario select q).ToList();
                                if (lstUsuarioPerfil.Count == 1)
                                {
                                    MoSesion.NumeroDeUsuario = lstUsuario.FirstOrDefault().NumeroDeUsuario;
                                    Ips.NumeroDeUsuario = MoSesion.NumeroDeUsuario;
                                    lstIPRegistrada.Add(Ips);
                                    MoSesion.NombreDeUsuario = lstUsuario.FirstOrDefault().NombreDeUsuario;
                                    MoSesion.NombreDePersona = lstUsuario.FirstOrDefault().NombreDePersona;
                                    MoSesion.ApellidoPaterno = lstUsuario.FirstOrDefault().ApellidoPaterno;
                                    MoSesion.ApellidoMaterno = lstUsuario.FirstOrDefault().ApellidoMaterno;
                                    MoSesion.IPDeUsuario = lstIPRegistrada.FirstOrDefault().IP;
                                    MoSesion.TerminalDeUsuario = lstIPRegistrada.FirstOrDefault().Terminal;
                                    MoSesion.CveTipoDeAcceso = lstIPRegistrada.FirstOrDefault().CveTipoDeAcceso;
                                    MoSesion.TextoTipoDeAccedo = lstIPRegistrada.FirstOrDefault().TextoTipoDeAccedo;
                                    MoSesion.NumeroDePerfil = lstUsuarioPerfil.FirstOrDefault().NumeroDePerfil;
                                    MoSesion.NombreDePerfil = lstUsuarioPerfil.FirstOrDefault().NombreDePerfil;
                                    MoSesion.Browser = Request.Browser.Browser + " " + Request.Browser.Version;
                                    if (!MvcApplication.ListaDeSesionesActivas.ContainsKey(MoSesion.IDSesion))
                                    {
                                        MvcApplication.ListaDeSesionesActivas.Add(MoSesion.IDSesion, new SesionActiva() { NumeroDeUsuario = MoSesion.NumeroDeUsuario, NumeroDePerfil = MoSesion.NumeroDePerfil, Deslogueado = false });
                                    }
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    ViewData["Informacion"] = Recursos.Textos.Account_ValidaPerfil;
                                }
                            }
                        }
                    }
                    else
                    {
                        ViewData["Informacion"] = Recursos.Textos.Account_ValidaUsuarioContraseña;
                    }
                }
            }
            catch (Exception e)
            {
                ViewData["Informacion"] = Recursos.Textos.Bitacora_TextoTryCatchGenerico;
                ClsBitacora.GeneraBitacora(NumeroDePantalla, 1, "Login", String.Format(Recursos.Textos.Bitacora_TextoDeError, e.GetType().ToString(), e.Message.Trim(), e.GetHashCode().ToString()));
                MoSesion.LimpiaSesion();
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