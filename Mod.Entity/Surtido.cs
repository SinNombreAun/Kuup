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
    
    public partial class Surtido
    {
        public short SUR_FOLIO_SURTIDO { get; set; }
        public byte SUR_NUM_PROVEEDOR { get; set; }
        public short SUR_NUM_PRODUCTO { get; set; }
        public string SUR_CODIGO_BARRAS { get; set; }
        public short SUR_CANT_NUEVA { get; set; }
        public decimal SUR_PRECIO_UNITARIO { get; set; }
        public decimal SUR_COSTO_TOTAL { get; set; }
        public System.DateTime SUR_FECHA_SURTIDO { get; set; }
        public byte SUR_CVE_ESTATUS { get; set; }
    
        public virtual Producto Producto { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
