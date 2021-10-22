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
using System.ComponentModel.DataAnnotations;

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
                    if (MvcApplication.ObjVersion.NumeroDeVersion.Contains("Demo"))
                    {
                        MoSesion.Demo = true;
                    }
                    else
                    {
                        MoSesion.Demo = false;
                    }
                    Account.IP = ClsAdicional.ClsGetHostNameAndIP.GetIPv4Address(Request.UserHostAddress);
                    Account.Terminal = ClsAdicional.ClsGetHostNameAndIP.GetHostName(Request.UserHostAddress);
                    ViewData["Informacion"] = String.Empty;
                    List<ClsUsuarios> lstUsuario = (from q in ClsUsuarios.getList() where q.NombreDeUsuario == Account.NombreDeUsuario && MoCifrado.Descifrado(q.PasswordUsuario) == Account.Password && q.CveDeEstatus == 1 select q).ToList();
                    if (lstUsuario.Count == 1)
                    {
                        if (!MoSesion.Demo)
                        {
                            List<ClsParametros> ParametroV = (from q in ClsParametros.getList() where q.NombreDeParametro == "FechaVencimientoKuup" select q).ToList();
                            if (ParametroV.Count() != 0)
                            {
                                if (DateTime.Now.ToString("") == ParametroV.FirstOrDefault().ValorDeParametro)
                                {
                                    return RedirectToAction("Vencimiento", "Account", new { lstUsuario.FirstOrDefault().NumeroDeUsuario });
                                }
                            }
                        }
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
                                ClsIPRegistradas Ips = new ClsIPRegistradas
                                {
                                    IP = Account.IP,
                                    Terminal = Account.Terminal,
                                    CveTipoDeAcceso = 1
                                };
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
        public ActionResult Contacto()
        {
            Contacto contacto = new Contacto();
            contacto.Asunto = "Acceso";
            contacto.Mensaje = "Por este medio, me permito solicitar acceso a la demo";
            return View(contacto);
        }
        [HttpPost]
        public ActionResult Contacto(Contacto Registro)
        {
            if (ModelState.IsValid)
            {
                ClsAdicional.ClsResultado Resultado = new ClsAdicional.ClsResultado(true, String.Empty);
                List<ClsParametros> Parametro = (from q in ClsParametros.getList() where q.CveTipo == 3 select q).ToList();
                ClsAdicional.EnvioDeCorreos envio = new ClsAdicional.EnvioDeCorreos(Parametro.Where(x => x.NombreDeParametro == "CorreosContacto").Select(y => y.ValorDeParametro).FirstOrDefault(), Registro.Asunto, Registro.Mensaje);
                String ContenidoPlantilla = envio.PlantillaHtml("EnvioKuup.html");
                if (!String.IsNullOrEmpty(ContenidoPlantilla))
                {
                    ContenidoPlantilla = ContenidoPlantilla.Replace("#Contenido#", Registro.Mensaje);
                    ContenidoPlantilla = ContenidoPlantilla.Replace("#DeclaratoriaFooter#", Parametro.Where(x => x.NombreDeParametro == "DeclaratoriaFooter").Select(y => y.Descripcion).FirstOrDefault());
                    ContenidoPlantilla = ContenidoPlantilla.Replace("#DireccionWebEmpresa#", Parametro.Where(x => x.NombreDeParametro == "DireccionWebEmpresa").Select(y => y.ValorDeParametro).FirstOrDefault());
                    ContenidoPlantilla = ContenidoPlantilla.Replace("#NombreDeLaEmpresa#", Parametro.Where(x => x.NombreDeParametro == "NombreDeLaEmpresa").Select(y => y.ValorDeParametro).FirstOrDefault());
                    ContenidoPlantilla = ContenidoPlantilla.Replace("#RutaDeLogoParaReportes#", Parametro.Where(x => x.NombreDeParametro == "RutaDeLogoParaReportes").Select(y => y.ValorDeParametro).FirstOrDefault());
                    envio.Mensaje = ContenidoPlantilla;
                }
                Dictionary<String, String> imagen = new Dictionary<string, string>();
                imagen.Add("RutaDeLogoParaReportes", Server.MapPath("~/Content/Imagenes/Kuup/Firma correo.png"));
                Resultado = envio.EnviarCorreo(imagen);
                if (Resultado.Resultado)
                {
                    ClsUsuarios usuario = new ClsUsuarios();
                    usuario.NombreDeUsuario = usuario.UsuarioParaDemo();//Registro.ApellidoPaterno.ToCharArray()[0].ToString().ToUpper() + Registro.Nombre.Split(' ')[0].ToUpper();
                    usuario.NombreDePersona = Registro.Nombre.Trim();
                    usuario.ApellidoPaterno = Registro.ApellidoPaterno.Trim();
                    usuario.ApellidoMaterno = Registro.ApellidoMaterno.Trim();
                    usuario.CorreoDeUsuario = Registro.Correo.Trim();
                    usuario.FechaDeRegistro = DateTime.Now;
                    usuario.PasswordUsuario = MoCifrado.Cifrado(Registro.Nombre.Trim().ToCharArray()[0].ToString().ToUpper() + Registro.ApellidoPaterno.Trim().ToCharArray()[0].ToString().ToUpper() + Registro.ApellidoPaterno.Trim().Substring(1, Registro.ApellidoPaterno.Trim().Length - 1) + "001"); 
                    usuario.CveDeEstatus = 1;
                    if (usuario.Insert())
                    {
                        byte PerfilDemo = (from q in ClsPerfiles.getList() where q.NombreDePerfil.ToUpper() == "DEMO" select q.NumeroDePerfil).FirstOrDefault();
                        ClsUsuariosPerfil AsignaPerfil = new ClsUsuariosPerfil()
                        {
                            NumeroDePerfil = PerfilDemo,
                            NumeroDeUsuario = usuario.NumeroDeUsuario,
                            CveDeEstatus = 1
                        };
                        if (AsignaPerfil.Insert())
                        {
                            envio = new ClsAdicional.EnvioDeCorreos(Registro.Correo, Registro.Asunto, Registro.Mensaje);
                            ContenidoPlantilla = envio.PlantillaHtml("EnvioKuup.html");
                            if (!String.IsNullOrEmpty(ContenidoPlantilla))
                            {
                                ContenidoPlantilla = ContenidoPlantilla.Replace("#Contenido#", String.Format(Parametro.Where(x => x.NombreDeParametro == "MensajeParaUsuario").Select(y => y.Descripcion).FirstOrDefault(), usuario.NombreDeUsuario, MoCifrado.Descifrado(usuario.PasswordUsuario)));
                                ContenidoPlantilla = ContenidoPlantilla.Replace("#DeclaratoriaFooter#", Parametro.Where(x => x.NombreDeParametro == "DeclaratoriaFooter").Select(y => y.Descripcion).FirstOrDefault());
                                ContenidoPlantilla = ContenidoPlantilla.Replace("#DireccionWebEmpresa#", Parametro.Where(x => x.NombreDeParametro == "DireccionWebEmpresa").Select(y => y.ValorDeParametro).FirstOrDefault());
                                ContenidoPlantilla = ContenidoPlantilla.Replace("#NombreDeLaEmpresa#", Parametro.Where(x => x.NombreDeParametro == "NombreDeLaEmpresa").Select(y => y.ValorDeParametro).FirstOrDefault());
                                ContenidoPlantilla = ContenidoPlantilla.Replace("#RutaDeLogoParaReportes#", Parametro.Where(x => x.NombreDeParametro == "RutaDeLogoParaReportes").Select(y => y.ValorDeParametro).FirstOrDefault());
                                envio.Mensaje = ContenidoPlantilla;
                            }
                            imagen = new Dictionary<string, string>();
                            imagen.Add("RutaDeLogoParaReportes", Server.MapPath("~/Content/Imagenes/Kuup/Firma correo.png"));
                            Resultado = envio.EnviarCorreo(imagen);
                            if (Resultado.Resultado)
                            {
                                ViewData["Informacion"] = "Se le ha enviado usuario y contraseña para el acceso a la Demo";
                                return View("EnvioDeSolicitud");
                            }
                            else
                            {
                                ViewData["Informacion"] = "No fue posible realizar el envió de usuario y contraseña para su acceso a la Demo";
                            }
                        }
                    }
                }
                if (!Resultado.Resultado)
                {
                    ClsBitacora.GeneraBitacora(NumeroDePantalla, 1, "EnviorCorreo", Resultado.Mensaje);
                }
            }
            else
            {
                ViewData["Informacion"] = "Los registros no son válidos para la solicitud";
            }
            return View(Registro);
        }
        [HttpGet]
        public ActionResult LoginOut()
        {
            if (MvcApplication.ObjVersion.NumeroDeVersion.ToUpper().Contains("DEMO"))
            {
                ClsUsuarios usurio = new ClsUsuarios();
                usurio.BajaUsuarioDemo(MoSesion.NumeroDeUsuario,MoSesion.NombreDeUsuario);
            }
            MoSesion.LimpiaSesion();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public ActionResult Vencimiento(int NumeroDeUsuario = 0)
        {
            if (NumeroDeUsuario != 0)
            {
                List<ClsUsuarios> lstUsuario = (from q in ClsUsuarios.getList() where q.NumeroDeUsuario == NumeroDeUsuario select q).ToList();
                if (lstUsuario.Count() != 0)
                {
                    ViewData["NombreDeUsuario"] = (lstUsuario.FirstOrDefault().NombreDePersona + " " + lstUsuario.FirstOrDefault().ApellidoPaterno + " " + lstUsuario.FirstOrDefault().ApellidoMaterno).Trim();
                }
                else
                {
                    ViewData["NombreDeUsuario"] = String.Empty;
                }
            }
            else
            {
                ViewData["NombreDeUsuario"] = String.Empty;
            }
            return View();
        }
    }
    public class Contacto
    {
        [Required, EmailAddress]
        public String Correo { get; set; }
        [Required]
        public String Nombre { get; set; }
        public String ApellidoPaterno { get; set; }
        public String ApellidoMaterno { get; set; }
        [Required]
        public String Asunto { get; set; }
        public String Mensaje { get; set; }
    }
}