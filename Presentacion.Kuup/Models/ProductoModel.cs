using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Web.Management;

namespace Presentacion.Kuup.Models
{
    public class ProductoModel : Negocio.Kuup.Clases.ClsProductos
    {
        public ProductoModel() { }
        public ProductoModel(Negocio.Kuup.Clases.ClsProductos Producto)
        {
            this.NumeroDeProducto = Producto.NumeroDeProducto;
            this.CodigoDeBarras = Producto.CodigoDeBarras;
            this.FechaDeRegistro = Producto.FechaDeRegistro;
            this.CantidadDeProductoUltima = Producto.CantidadDeProductoUltima;
            this.CantidadDeProductoNueva = Producto.CantidadDeProductoNueva;
            this.CantidadDeProductoTotal = Producto.CantidadDeProductoTotal;
            this.NombreDeProducto = Producto.NombreDeProducto;
            this.Descripcion = Producto.Descripcion;
            this.CveAviso = Producto.CveAviso;
            this.CveCorreoSurtido = Producto.CveCorreoSurtido;
            this.CantidadMinima = Producto.CantidadMinima;
            this.NumeroDeProveedor = Producto.NumeroDeProveedor;
            this.PrecioUnitario = Producto.PrecioUnitario;
            this.CveAplicaMayoreo = Producto.CveAplicaMayoreo;
            this.CantidadMinimaMayoreo = Producto.CantidadMinimaMayoreo;
            this.PrecioMayoreo = Producto.PrecioMayoreo;
            this.CveEstatus = Producto.CveEstatus;
            this.TextoAviso = Producto.TextoAviso;
            this.TextoCorreoSurtido = Producto.TextoCorreoSurtido;
            this.NombreDeProveedor = Producto.NombreDeProveedor;
            this.TextoAplicaMayoreo = Producto.TextoAplicaMayoreo;
            this.TextoEstatus = Producto.TextoEstatus;
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_NumeroDeProducto")]
        public short fNumeroDeProducto
        {
            get { return this.NumeroDeProducto; }
            set { this.NumeroDeProducto = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CodigoDeBarras")]
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_FechaDeRegistro")]
        public DateTime fFechaDeRegistro
        {
            get { return this.FechaDeRegistro; }
            set { this.FechaDeRegistro = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CantidadDeProductoUltima")]
        public short fCantidadDeProductoUltima
        {
            get { return this.CantidadDeProductoUltima; }
            set { this.CantidadDeProductoUltima = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CantidadDeProductoNueva")]
        public short fCantidadDeProductoNueva
        {
            get { return this.CantidadDeProductoNueva; }
            set { this.CantidadDeProductoNueva = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CantidadDeProductoTotal")]
        public short fCantidadDeProductoTotal
        {
            get { return this.CantidadDeProductoTotal; }
            set { this.CantidadDeProductoTotal = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_NombreDeProducto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_Descripcion")]
        public String fDescripcion
        {
            get { return this.Descripcion; }
            set { this.Descripcion = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CveAviso")]
        public byte fCveAviso
        {
            get { return this.CveAviso; }
            set { this.CveAviso = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CveCorreoSurtido")]
        public byte fCveCorreoSurtido
        {
            get { return this.CveCorreoSurtido; }
            set { this.CveCorreoSurtido = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CantidadMinima")]
        public short fCantidadMinima
        {
            get { return this.CantidadMinima; }
            set { this.CantidadMinima = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_NumeroDeProveedor")]
        public Nullable<byte> fNumeroDeProveedor
        {
            get { return this.NumeroDeProveedor; }
            set { this.NumeroDeProveedor = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_PrecioUnitario")]
        public decimal fPrecioUnitario
        {
            get { return this.PrecioUnitario; }
            set { this.PrecioUnitario = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CveAplicaMayoreo")]
        public byte fCveAplicaMayoreo
        {
            get { return this.CveAplicaMayoreo; }
            set { this.CveAplicaMayoreo = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CantidadMinimaMayoreo")]
        public Nullable<short> fCantidadMinimaMayoreo
        {
            get { return this.CantidadMinimaMayoreo; }
            set { this.CantidadMinimaMayoreo = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_PrecioMayoreo")]
        public Nullable<decimal> fPrecioMayoreo
        {
            get { return this.PrecioMayoreo; }
            set { this.PrecioMayoreo = value; }
        }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CveEstatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_TextoAviso")]
        public String fTextoAviso
        {
            get { return this.TextoAviso; }
            set { this.TextoAviso = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_TextoCorreoSurtido")]
        public String fTextoCorreoSurtido
        {
            get { return this.TextoCorreoSurtido; }
            set { this.TextoCorreoSurtido = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_NombreDeProveedor")]
        public String fNombreDeProveedor
        {
            get { return this.NombreDeProveedor; }
            set { this.NombreDeProveedor = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_TextoAplicaMayoreo")]
        public String fTextoAplicaMayoreo
        {
            get { return this.TextoAplicaMayoreo; }
            set { this.TextoAplicaMayoreo = value; }
        }
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_TextoEstatus")]
        public String fTextoEstatus
        {
            get { return this.TextoEstatus; }
            set { this.TextoEstatus = value; }
        }
    }
}