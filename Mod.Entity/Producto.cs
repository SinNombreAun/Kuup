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
    
    public partial class Producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producto()
        {
            this.CodigoDeBarras = new HashSet<CodigoDeBarras>();
            this.Venta = new HashSet<Venta>();
            this.ConfiguraMayoreo = new HashSet<ConfiguraMayoreo>();
            this.Surtido = new HashSet<Surtido>();
            this.ConfiguraPaquete = new HashSet<ConfiguraPaquete>();
            this.ConfiguraPaquete1 = new HashSet<ConfiguraPaquete>();
        }
    
        public short PRO_NUM_PRODUCTO { get; set; }
        public string PRO_CODIGO_BARRAS { get; set; }
        public System.DateTime PRO_FECHA_REGISTRO { get; set; }
        public short PRO_CANT_PRODUCTO_ULTIMA { get; set; }
        public short PRO_CANT_PRODUCTO_NUEVA { get; set; }
        public short PRO_CANT_PRODUCTO_TOTAL { get; set; }
        public string PRO_NOM_PRODUCTO { get; set; }
        public string PRO_DESCRIPCION { get; set; }
        public byte PRO_CVE_AVISO { get; set; }
        public byte PRO_CVE_CORREO_SURTIDO { get; set; }
        public short PRO_CAT_MINIMA { get; set; }
        public Nullable<byte> PRO_NUM_PROVEEDOR { get; set; }
        public decimal PRO_PRECIO_UNITARIO { get; set; }
        public byte PRO_CVE_ESTATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CodigoDeBarras> CodigoDeBarras { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Venta> Venta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfiguraMayoreo> ConfiguraMayoreo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Surtido> Surtido { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfiguraPaquete> ConfiguraPaquete { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ConfiguraPaquete> ConfiguraPaquete1 { get; set; }
    }
}
