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
    
    public partial class Pantalla
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pantalla()
        {
            this.Funcionalidad = new HashSet<Funcionalidad>();
            this.BitacoraCorreo = new HashSet<BitacoraCorreo>();
            this.PantallaPerfil = new HashSet<PantallaPerfil>();
            this.Bitacora = new HashSet<Bitacora>();
        }
    
        public short PAN_NUM_PANTALLA { get; set; }
        public string PAN_NOM_PANTALLA { get; set; }
        public string PAN_NOM_PANTALLA_INT { get; set; }
        public string PAN_DESCRIPCION { get; set; }
        public byte PAN_CVE_MANEJO_INTERNO { get; set; }
        public string PAN_LLAVE { get; set; }
        public byte PAN_CVE_ESTATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Funcionalidad> Funcionalidad { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BitacoraCorreo> BitacoraCorreo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PantallaPerfil> PantallaPerfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bitacora> Bitacora { get; set; }
    }
}
