using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Nucleo.Motores
{
    public static class ClsConfiguracion
    {
        private static readonly String _RutaCodigoDeBarras = "~/Imagenes/CodigoBarras";
        public static String RutaCodigoDeBarras
        {
            get { return _RutaCodigoDeBarras; }
        }
        private static readonly String _RutaDescarga = "~/App_Data/Download/";
        public static String RutaDescarga
        {
            get { return _RutaDescarga; }
        }
        private static readonly String _RutaUpload = "~/App_Data/Upload/";
        public static String RutaUpload
        {
            get { return _RutaUpload; }
        }
    }
}