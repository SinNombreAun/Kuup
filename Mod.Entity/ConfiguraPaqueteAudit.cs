//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mod.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConfiguraPaqueteAudit
    {
        public short CNP_ID_AUDIT { get; set; }
        public string CNP_TERMINAL { get; set; }
        public string CNP_IP { get; set; }
        public string CNP_VERSION { get; set; }
        public string CNP_NOM_USUARIO { get; set; }
        public System.DateTime CNP_FECHA_BASE { get; set; }
        public string CNP_NOM_FUNCIONALIDAD { get; set; }
        public short CNP_NUM_PRODUCTO_PADRE { get; set; }
        public string CNP_CODIGO_BARRAS_PADRE { get; set; }
        public short CNP_NUM_PRODUCTO_HIJO { get; set; }
        public string CNP_CODIGO_BARRAS_HIJO { get; set; }
        public decimal CNP_PRECIO_PRODUCTO_PADRE { get; set; }
        public byte CNP_CANTIDAD_A_SALIR { get; set; }
        public Nullable<decimal> CNP_PRECIO_PRODUCTO_HIJO { get; set; }
        public decimal CNP_IMPORTE_TOTAL { get; set; }
        public string CNP_NOM_PRODUCTO_PADRE { get; set; }
        public string CNP_NOM_PRODUCTO_HIJO { get; set; }
    }
}