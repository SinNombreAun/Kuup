using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Nucleo.Clases
{
    public class ClsVersion
    {
        private byte VersionMayor { get; set; }
        private byte VersionDeCompativilidad { get; set; }
        private byte VersionDeCompilado { get; set; }
        private String Complemento { get; set; }
        public DateTime FechaDeVersion { get; set; }
        public String NumeroDeVersion { get; set; }
        public String NumeroDeVersionCorta { get; set; }
        public String TextoDeVersion { get; set; }
        public ClsVersion()
        {
            VersionMayor = 1;
            VersionDeCompativilidad = 1;
            VersionDeCompilado = 2;
            Complemento = "stable-Demo";
            FechaDeVersion = new DateTime(2021,3,30);
            NumeroDeVersion = String.Format("{0}.{1}.{2}-{3}",VersionMayor,VersionDeCompativilidad,VersionDeCompilado,Complemento);
            NumeroDeVersionCorta = String.Format("{0}.{1}.{2}", VersionMayor, VersionDeCompativilidad, VersionDeCompilado);
            TextoDeVersion = String.Format("Versión {0}", NumeroDeVersion);
        }
    }
}