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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBKuupEntities : DbContext
    {
        public DBKuupEntities()
            : base("name=DBKuupEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<Funcionalidad> Funcionalidad { get; set; }
        public virtual DbSet<FuncionPerfil> FuncionPerfil { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<PantallaPerfil> PantallaPerfil { get; set; }
        public virtual DbSet<Parametro> Parametro { get; set; }
        public virtual DbSet<Perfil> Perfil { get; set; }
        public virtual DbSet<Proveedor> Proveedor { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }
        public virtual DbSet<BitacoraCorreo> BitacoraCorreo { get; set; }
        public virtual DbSet<ViAgenda> ViAgenda { get; set; }
        public virtual DbSet<ViBitacoraCorreo> ViBitacoraCorreo { get; set; }
        public virtual DbSet<ViMenu> ViMenu { get; set; }
        public virtual DbSet<ViPantalla> ViPantalla { get; set; }
        public virtual DbSet<ViPantallaPerfil> ViPantallaPerfil { get; set; }
        public virtual DbSet<ViParametro> ViParametro { get; set; }
        public virtual DbSet<ViPerfil> ViPerfil { get; set; }
        public virtual DbSet<ViProveedor> ViProveedor { get; set; }
        public virtual DbSet<ViUsuario> ViUsuario { get; set; }
        public virtual DbSet<ViUsuarioPerfil> ViUsuarioPerfil { get; set; }
        public virtual DbSet<ViCodigoDeBarras> ViCodigoDeBarras { get; set; }
        public virtual DbSet<CodigoDeBarras> CodigoDeBarras { get; set; }
        public virtual DbSet<Pantalla> Pantalla { get; set; }
        public virtual DbSet<IPRegistradas> IPRegistradas { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<ViIPRegistradas> ViIPRegistradas { get; set; }
        public virtual DbSet<Bitacora> Bitacora { get; set; }
        public virtual DbSet<ViBitacora> ViBitacora { get; set; }
        public virtual DbSet<VentaTotal> VentaTotal { get; set; }
        public virtual DbSet<ViVentaTotal> ViVentaTotal { get; set; }
        public virtual DbSet<ConfiguraMayoreo> ConfiguraMayoreo { get; set; }
        public virtual DbSet<ViConfiguraMayoreo> ViConfiguraMayoreo { get; set; }
        public virtual DbSet<Surtido> Surtido { get; set; }
        public virtual DbSet<ViSurtido> ViSurtido { get; set; }
        public virtual DbSet<ViFuncionalidad> ViFuncionalidad { get; set; }
        public virtual DbSet<ViFuncionPerfil> ViFuncionPerfil { get; set; }
        public virtual DbSet<ViVenta> ViVenta { get; set; }
        public virtual DbSet<ViConfiguraPaquete> ViConfiguraPaquete { get; set; }
        public virtual DbSet<ConfiguraPaquete> ConfiguraPaquete { get; set; }
        public virtual DbSet<ConfiguraMayoreoAudit> ConfiguraMayoreoAudit { get; set; }
        public virtual DbSet<ConfiguraPaqueteAudit> ConfiguraPaqueteAudit { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<ViMarca> ViMarca { get; set; }
        public virtual DbSet<MarcaAudit> MarcaAudit { get; set; }
        public virtual DbSet<AsignaMarca> AsignaMarca { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoAudit> ProductoAudit { get; set; }
        public virtual DbSet<TipoProducto> TipoProducto { get; set; }
        public virtual DbSet<TipoProductoAudit> TipoProductoAudit { get; set; }
        public virtual DbSet<ViAsignaMarca> ViAsignaMarca { get; set; }
        public virtual DbSet<ViTipoProducto> ViTipoProducto { get; set; }
        public virtual DbSet<ViProducto> ViProducto { get; set; }
        public virtual DbSet<Claves> Claves { get; set; }
        public virtual DbSet<ViClaves> ViClaves { get; set; }
    
        public virtual ObjectResult<VentasTotalesDetalle_Result> VentasTotalesDetalle(Nullable<System.DateTime> fechaInicio, Nullable<System.DateTime> fechaFin, Nullable<short> folioOperacion)
        {
            var fechaInicioParameter = fechaInicio.HasValue ?
                new ObjectParameter("fechaInicio", fechaInicio) :
                new ObjectParameter("fechaInicio", typeof(System.DateTime));
    
            var fechaFinParameter = fechaFin.HasValue ?
                new ObjectParameter("fechaFin", fechaFin) :
                new ObjectParameter("fechaFin", typeof(System.DateTime));
    
            var folioOperacionParameter = folioOperacion.HasValue ?
                new ObjectParameter("folioOperacion", folioOperacion) :
                new ObjectParameter("folioOperacion", typeof(short));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<VentasTotalesDetalle_Result>("VentasTotalesDetalle", fechaInicioParameter, fechaFinParameter, folioOperacionParameter);
        }
    
        public virtual ObjectResult<NoAsignadosPorOrigen_Result> NoAsignadosPorOrigen(Nullable<short> registro, string origen)
        {
            var registroParameter = registro.HasValue ?
                new ObjectParameter("registro", registro) :
                new ObjectParameter("registro", typeof(short));
    
            var origenParameter = origen != null ?
                new ObjectParameter("origen", origen) :
                new ObjectParameter("origen", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<NoAsignadosPorOrigen_Result>("NoAsignadosPorOrigen", registroParameter, origenParameter);
        }
    }
}
