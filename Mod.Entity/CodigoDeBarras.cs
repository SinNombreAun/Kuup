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
    
    public partial class CodigoDeBarras
    {
        public string COB_CODIGO_BARRAS { get; set; }
        public short COB_NUM_PRODUCTO { get; set; }
        public string COB_RUTA_ARCHIVO { get; set; }
        public System.DateTime COB_FECHA_GENERACION { get; set; }
        public byte COB_CVE_ESTATUS { get; set; }
    
        public virtual Producto Producto { get; set; }
    }
}
