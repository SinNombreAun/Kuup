using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Funciones.Kuup.Adicionales;

namespace Presentacion.Kuup.Nucleo.Motores
{
    public static class MoSesion
    {
        public static void LimpiaSesion()
        {
            MoSesion.NumeroDeUsuario = 0;
            MoSesion.NombreDeUsuario = String.Empty;
            MoSesion.NombreDePersona = String.Empty;
            MoSesion.ApellidoPaterno = String.Empty;
            MoSesion.ApellidoMaterno = String.Empty;
            MoSesion.IPDeUsuario = String.Empty;
            MoSesion.TerminalDeUsuario = String.Empty;
            MoSesion.CveTipoDeAcceso = 0;
            MoSesion.TextoTipoDeAccedo = String.Empty;
            MoSesion.NumeroDePerfil = 0;
            MoSesion.NombreDePerfil = String.Empty;
            MoSesion.Browser = String.Empty;
        }
        public static short NumeroDeUsuario
        {
            get { return ClsAdicional.Convert<short>(HttpContext.Current.Session["NumeroDeUsuario"].ToString()); }
            set { HttpContext.Current.Session["NumeroDeUsuario"] = value.ToString(); }
        }
        public static String NombreDeUsuario
        {
            get { return (String) HttpContext.Current.Session["NombreDeUsuario"]; }
            set { HttpContext.Current.Session["NombreDeUsuario"] = value; }
        }
        public static String NombreDePersona
        {
            get { return (String)HttpContext.Current.Session["NombreDePersona"]; }
            set { HttpContext.Current.Session["NombreDePersona"] = value; }
        }
        public static String ApellidoPaterno
        {
            get { return (String)HttpContext.Current.Session["ApellidoPaterno"]; }
            set { HttpContext.Current.Session["ApellidoPaterno"] = value; }
        }
        public static String ApellidoMaterno
        {
            get { return (String)HttpContext.Current.Session["ApellidoMaterno"]; }
            set { HttpContext.Current.Session["ApellidoMaterno"] = value; }
        }
        public static String IPDeUsuario
        {
            get { return (String) HttpContext.Current.Session["IPDeUsuario"]; }
            set { HttpContext.Current.Session["IPDeUsuario"] = value; }
        }
        public static String TerminalDeUsuario
        {
            get { return (String) HttpContext.Current.Session["TerminalDeUsuario"]; }
            set { HttpContext.Current.Session["TerminalDeUsuario"] = value; }
        }
        public static byte NumeroDePerfil
        {
            get { return ClsAdicional.Convert<byte>(HttpContext.Current.Session["NumeroDePerfil"].ToString()); }
            set { HttpContext.Current.Session["NumeroDePerfil"] = value.ToString(); }
        }
        public static String NombreDePerfil
        {
            get { return (String) HttpContext.Current.Session["NombreDePerfil"]; }
            set { HttpContext.Current.Session["NombreDePerfil"] = value; }
        }
        public static byte CveTipoDePerfil
        {
            get { return ClsAdicional.Convert<byte>(HttpContext.Current.Session["CveTipoDePerfil"].ToString()); }
            set { HttpContext.Current.Session["CveTipoDePerfil"] = value.ToString(); }
        }
        public static byte CveTipoDeAcceso
        {
            get { return ClsAdicional.Convert<byte>(HttpContext.Current.Session["CveTipoDeAcceso"].ToString()); }
            set { HttpContext.Current.Session["CveTipoDeAcceso"] = value.ToString(); }
        }
        public static String TextoTipoDeAccedo
        {
            get { return (String) HttpContext.Current.Session["TextoTipoDeAccedo"]; }
            set { HttpContext.Current.Session["TextoTipoDeAccedo"] = value; }
        }
        public static String Browser
        {
            get { return (String)HttpContext.Current.Session["Browser"]; }
            set { HttpContext.Current.Session["Browser"] = value; }
        }
    }
}