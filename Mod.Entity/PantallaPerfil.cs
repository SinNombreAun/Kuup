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
    
    public partial class PantallaPerfil
    {
        public short PAP_NUM_PANTALLA { get; set; }
        public byte PAP_NUM_PERFIL { get; set; }
        public byte PAP_CVE_ESTATUS { get; set; }
    
        public virtual Perfil Perfil { get; set; }
        public virtual Pantalla Pantalla { get; set; }
    }
}
