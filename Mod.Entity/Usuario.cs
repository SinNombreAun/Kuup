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
    
    public partial class Usuario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuario()
        {
            this.Agenda = new HashSet<Agenda>();
            this.UsuarioPerfil = new HashSet<UsuarioPerfil>();
            this.IPRegistradas = new HashSet<IPRegistradas>();
            this.VentaTotal = new HashSet<VentaTotal>();
        }
    
        public short USU_NUM_USUARIO { get; set; }
        public string USU_NOM_USUARIO { get; set; }
        public string USU_NOM_PERS { get; set; }
        public string USU_APP_PERS { get; set; }
        public string USU_APM_PERS { get; set; }
        public string USU_CORREO { get; set; }
        public string USU_PASSWORD { get; set; }
        public System.DateTime USU_FECHA_REGISTRO { get; set; }
        public Nullable<System.DateTime> USU_FECHA_CANCELACION { get; set; }
        public byte USU_CVE_ESTATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Agenda> Agenda { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IPRegistradas> IPRegistradas { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VentaTotal> VentaTotal { get; set; }
    }
}
