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
    
    public partial class Perfil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Perfil()
        {
            this.FuncionPerfil = new HashSet<FuncionPerfil>();
            this.PantallaPerfil = new HashSet<PantallaPerfil>();
            this.UsuarioPerfil = new HashSet<UsuarioPerfil>();
        }
    
        public byte PER_NUM_PERFIL { get; set; }
        public string PER_NOM_PERFIL { get; set; }
        public byte PER_CVE_TIPO_PERFIL { get; set; }
        public byte PER_CVE_ESTATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FuncionPerfil> FuncionPerfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PantallaPerfil> PantallaPerfil { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }
    }
}
