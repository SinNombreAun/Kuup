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
    
    public partial class Agenda
    {
        public short AGN_NUM_AGENDA { get; set; }
        public System.DateTime AGN_FECHA_ALTA { get; set; }
        public System.DateTime AGN_FECHA_INI_EVENTO { get; set; }
        public System.DateTime AGN_FECHA_FIN_EVENTO { get; set; }
        public short AGN_NUM_USUARIO { get; set; }
        public byte AGN_CVE_NOTIFICA { get; set; }
        public string AGN_DESCRIPCION { get; set; }
        public byte AGN_CVE_ESTATUS { get; set; }
    
        public virtual Usuario Usuario { get; set; }
    }
}
