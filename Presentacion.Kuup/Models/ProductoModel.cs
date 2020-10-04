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
        [Display(Name ="Número de Producto")]
        public short fNumeroDeProducto
        {
            get { return this.NumeroDeProducto; }
            set { this.NumeroDeProducto = value; }
        }
        [Required]
        [Display(Name ="Codigo de Barras")]
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Fecha de Registro")]
        public DateTime fFechaDeRegistro
        {
            get { return this.FechaDeRegistro; }
            set { this.FechaDeRegistro = value; }
        }
        [Required]
        [Display(Name = "Ultima Cantidad de Producto")]
        public short fCantidadDeProductoUltima
        {
            get { return this.CantidadDeProductoUltima; }
            set { this.CantidadDeProductoUltima = value; }
        }
        [Required]
        [Display(Name = "Nueva Cantidad de Producto")]
        public short fCantidadDeProductoNueva
        {
            get { return this.CantidadDeProductoNueva; }
            set { this.CantidadDeProductoNueva = value; }
        }
        [Required]
        [Display(Name = "Cantidad de Producto Total")]
        public short fCantidadDeProductoTotal
        {
            get { return this.CantidadDeProductoTotal; }
            set { this.CantidadDeProductoTotal = value; }
        }
        [Required]
        [Display(Name = "Nombre de Producto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
        [Display(Name = "Descripción")]
        public String fDescripcion
        {
            get { return this.Descripcion; }
            set { this.Descripcion = value; }
        }
        [Required]
        [Display(Name = "Aviso de Surtido")]
        public byte fCveAviso
        {
            get { return this.CveAviso; }
            set { this.CveAviso = value; }
        }
        [Required]
        [Display(Name = "Notificar Surtido por Correo")]
        public byte fCveCorreoSurtido
        {
            get { return this.CveCorreoSurtido; }
            set { this.CveCorreoSurtido = value; }
        }
        [Required]
        [Display(Name = "Cantidad minima a notificar")]
        public short fCantidadMinima
        {
            get { return this.CantidadMinima; }
            set { this.CantidadMinima = value; }
        }
        [Display(Name = "Proveedor")]
        public Nullable<byte> fNumeroDeProveedor
        {
            get { return this.NumeroDeProveedor; }
            set { this.NumeroDeProveedor = value; }
        }
        [Required]
        [Display(Name = "Precio unitario")]
        public decimal fPrecioUnitario
        {
            get { return this.PrecioUnitario; }
            set { this.PrecioUnitario = value; }
        }
        [Required]
        [Display(Name = "Aplica Mayoreo")]
        public byte fCveAplicaMayoreo
        {
            get { return this.CveAplicaMayoreo; }
            set { this.CveAplicaMayoreo = value; }
        }
        [Display(Name = "Cantidad minima para aplicar mayoreo")]
        public Nullable<short> fCantidadMinimaMayoreo
        {
            get { return this.CantidadMinimaMayoreo; }
            set { this.CantidadMinimaMayoreo = value; }
        }
        [Display(Name = "Precio al mayoreo")]
        public Nullable<decimal> fPrecioMayoreo
        {
            get { return this.PrecioMayoreo; }
            set { this.PrecioMayoreo = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Display(Name = "Aviso de Surtido")]
        public String fTextoAviso
        {
            get { return this.TextoAviso; }
            set { this.TextoAviso = value; }
        }
        [Display(Name = "Notificar Surtido por Correo")]
        public String fTextoCorreoSurtido
        {
            get { return this.TextoCorreoSurtido; }
            set { this.TextoCorreoSurtido = value; }
        }
        [Display(Name = "Proveedor")]
        public String fNombreDeProveedor
        {
            get { return this.NombreDeProveedor; }
            set { this.NombreDeProveedor = value; }
        }
        [Display(Name = "Aplica Mayoreo")]
        public String fTextoAplicaMayoreo
        {
            get { return this.TextoAplicaMayoreo; }
            set { this.TextoAplicaMayoreo = value; }
        }
        [Display(Name = "Estatus")]
        public String fTextoEstatus
        {
            get { return this.TextoEstatus; }
            set { this.TextoEstatus = value; }
        }
    }
}