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