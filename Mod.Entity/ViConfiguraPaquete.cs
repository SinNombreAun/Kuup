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
    
    public partial class ViConfiguraPaquete
    {
        public short CNP_NUM_PRODUCTO_PADRE { get; set; }
        public string CNP_CODIGO_BARRAS_PADRE { get; set; }
        public short CNP_NUM_PRODUCTO_HIJO { get; set; }
        public string CNP_CODIGO_BARRAS_HIJO { get; set; }
        public decimal CNP_PRECIO_PRODUCTO_PADRE { get; set; }
        public Nullable<decimal> CNP_PRECIO_PRODUCTO_HIJO { get; set; }
        public byte CNP_CANTIDAD_A_SALIR { get; set; }
        public decimal CNP_IMPORTE_TOTAL { get; set; }
        public string CNP_NOM_PRODUCTO_PADRE { get; set; }
        public string CNP_NOM_PRODUCTO_HIJO { get; set; }
    }
}
