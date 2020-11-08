using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Nucleo.Motores
{
    public class ClsEnumerables
    {
        public enum CveDeEstatusGeneral : byte
        {
            ACTIVO = 1,
            CANCELADO = 2
        }
        public enum CveDeEstatusVentas : byte
        {
            VENDIDA = 1,
            CANCELADA = 2,
            DEVOLUCION = 3
        }
    }
}