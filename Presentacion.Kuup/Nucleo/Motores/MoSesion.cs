using Funciones.Kuup.Adicionales;
using System;
using System.Web;

namespace Presentacion.Kuup.Nucleo.Motores
{
    public static class MoSesion
    {
        public static void LimpiaSesion()
        {
            if (MvcApplication.ListaDeSesionesActivas.ContainsKey(MoSesion.IDSesion))
            {
                MvcApplication.ListaDeSesionesActivas.Remove(MoSesion.IDSesion);
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.Clear();
            }
        }
        public static String IDSesion
        {
            get { return HttpContext.Current.Session.SessionID; }
        }
        public static short NumeroDeUsuario
        {
            get { return (HttpContext.Current.Session.Count == 0 ? (short)0 : ClsAdicional.Convert<short>(HttpContext.Current.Session["NumeroDeUsuario"].ToString()));}
            set { HttpContext.Current.Session["NumeroDeUsuario"] = value.ToString(); }
        }
        public static String NombreDeUsuario
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String) HttpContext.Current.Session["NombreDeUsuario"]); }
            set { HttpContext.Current.Session["NombreDeUsuario"] = value; }
        }
        public static String NombreDePersona
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String)HttpContext.Current.Session["NombreDePersona"]); }
            set { HttpContext.Current.Session["NombreDePersona"] = value; }
        }
        public static String ApellidoPaterno
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String)HttpContext.Current.Session["ApellidoPaterno"]); }
            set { HttpContext.Current.Session["ApellidoPaterno"] = value; }
        }
        public static String ApellidoMaterno
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String)HttpContext.Current.Session["ApellidoMaterno"]); }
            set { HttpContext.Current.Session["ApellidoMaterno"] = value; }
        }
        public static String IPDeUsuario
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String) HttpContext.Current.Session["IPDeUsuario"]); }
            set { HttpContext.Current.Session["IPDeUsuario"] = value; }
        }
        public static String TerminalDeUsuario
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String) HttpContext.Current.Session["TerminalDeUsuario"]); }
            set { HttpContext.Current.Session["TerminalDeUsuario"] = value; }
        }
        public static byte NumeroDePerfil
        {
            get { return (HttpContext.Current.Session.Count == 0 ? (byte)0 : ClsAdicional.Convert<byte>(HttpContext.Current.Session["NumeroDePerfil"].ToString())); }
            set { HttpContext.Current.Session["NumeroDePerfil"] = value.ToString(); }
        }
        public static String NombreDePerfil
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String) HttpContext.Current.Session["NombreDePerfil"]); }
            set { HttpContext.Current.Session["NombreDePerfil"] = value; }
        }
        public static byte CveTipoDePerfil
        {
            get { return (HttpContext.Current.Session.Count == 0 ? (byte)0 : ClsAdicional.Convert<byte>(HttpContext.Current.Session["CveTipoDePerfil"].ToString())); }
            set { HttpContext.Current.Session["CveTipoDePerfil"] = value.ToString(); }
        }
        public static byte CveTipoDeAcceso
        {
            get { return (HttpContext.Current.Session.Count == 0 ? (byte)0 : ClsAdicional.Convert<byte>(HttpContext.Current.Session["CveTipoDeAcceso"].ToString())); }
            set { HttpContext.Current.Session["CveTipoDeAcceso"] = value.ToString(); }
        }
        public static String TextoTipoDeAccedo
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String) HttpContext.Current.Session["TextoTipoDeAccedo"]); }
            set { HttpContext.Current.Session["TextoTipoDeAccedo"] = value; }
        }
        public static String Browser
        {
            get { return (HttpContext.Current.Session.Count == 0 ? String.Empty : (String)HttpContext.Current.Session["Browser"]); }
            set { HttpContext.Current.Session["Browser"] = value; }
        }
        public static  bool Demo
        {
            get { return (HttpContext.Current.Session.Count == 0 ? false : (bool)HttpContext.Current.Session["Demo"]); }
            set { HttpContext.Current.Session["Demo"] = value; }
        }
    }
    public class SesionActiva
    {
        public short NumeroDeUsuario { get; set; }
        public byte NumeroDePerfil { get; set; }
        public bool Deslogueado { get; set; }
    }
}