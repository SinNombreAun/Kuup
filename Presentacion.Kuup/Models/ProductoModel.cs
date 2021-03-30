using System;
using System.ComponentModel.DataAnnotations;

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
            this.NumeroDeTipoDeProducto = Producto.NumeroDeTipoDeProducto;
            this.NumeroDeMarca = Producto.NumeroDeMarca;
            this.CveAviso = Producto.CveAviso;
            this.CveCorreoSurtido = Producto.CveCorreoSurtido;
            this.CantidadMinima = Producto.CantidadMinima;
            this.NumeroDeProveedor = Producto.NumeroDeProveedor;
            this.PrecioUnitario = Producto.PrecioUnitario;
            this.CveDeEstatus = Producto.CveDeEstatus;
            this.NombreDeTipoDeProducto = Producto.NombreDeTipoDeProducto;
            this.NombreDeMarca = Producto.NombreDeMarca;
            this.TextoAviso = Producto.TextoAviso;
            this.TextoCorreoSurtido = Producto.TextoCorreoSurtido;
            this.NombreDeProveedor = Producto.NombreDeProveedor;
            this.TextoDeEstatus = Producto.TextoDeEstatus;
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
        [Display(Name = "Tipo de Producto")]
        public short fNumeroDeTipoDeProducto
        {
            get { return this.NumeroDeTipoDeProducto; }
            set { this.NumeroDeTipoDeProducto = value; }
        }
        [Display(Name = "Marca")]
        public Nullable<short> fNumeroDeMarca
        {
            get { return this.NumeroDeMarca; }
            set { this.NumeroDeMarca = value;}
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
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_CveDeEstatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Display(Name = "Tipo de Producto")]
        public String fNombreDeTipoDeProducto
        {
            get { return this.NombreDeTipoDeProducto; }
            set { this.NombreDeTipoDeProducto = value; }
        }
        [Display(Name ="Marca")]
        public String fNombreDeMarca
        {
            get { return this.NombreDeMarca; }
            set { this.NombreDeMarca = value; }
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
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Producto_TextoDeEstatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
    public class MayoreoProducto
    {
        public String NombreDeProducto { get; set; }
        public String CodigoDeBarras { get; set; }
        public short CveAplicaMayoreo { get; set; }
        public String TextoAplicaMayoreo { get; set; }
        public byte CantidadMinimaMayoreo { get; set; }
        public decimal PrecioMayoreo { get; set; }
    }
}