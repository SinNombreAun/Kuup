//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mod.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class ConfiguraMayoreoAudit
    {
        public short COM_ID_AUDIT { get; set; }
        public string COM_TERMINAL { get; set; }
        public string COM_IP { get; set; }
        public string COM_VERSION { get; set; }
        public string COM_NOM_USUARIO { get; set; }
        public System.DateTime COM_FECHA_BASE { get; set; }
        public string COM_NOM_FUNCIONALIDAD { get; set; }
        public short COM_NUM_MAYOREO { get; set; }
        public short COM_NUM_PRODUCTO { get; set; }
        public string COM_CODIGO_BARRAS { get; set; }
        public byte COM_CVE_APLICA_PAQUETES { get; set; }
        public byte COM_CANTIDAD_MINIMA { get; set; }
        public Nullable<byte> COM_CANTIDAD_MAXIMA { get; set; }
        public decimal COM_PRECIO_MAYOREO { get; set; }
        public string COM_NOM_PRODUCTO { get; set; }
    }
}
