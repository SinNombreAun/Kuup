using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class VentaModel : Negocio.Kuup.Clases.ClsVentas
    {
        public VentaModel() { }
        public VentaModel(Negocio.Kuup.Clases.ClsVentas Venta)
        {
            this.FolioDeOperacion = Venta.FolioDeOperacion;
            this.NumeroDeProducto = Venta.NumeroDeProducto;
            this.CodigoDeBarras = Venta.CodigoDeBarras;
            this.NumeroDeTipoDeProducto = Venta.NumeroDeTipoDeProducto;
            this.NumeroDeMarca = Venta.NumeroDeMarca;
            this.CantidadDeProducto = Venta.CantidadDeProducto;
            this.PrecioUnitario = Venta.PrecioUnitario;
            this.ImporteDeProducto = Venta.ImporteDeProducto;
            this.NombreDeProducto = Venta.NombreDeProducto;
            this.NombreDeTipoDeProducto = Venta.NombreDeTipoDeProducto;
            this.NombreDeMarca = Venta.NombreDeMarca;
        }
        [Required]
        [Display(Name = "Folio De Operacion")]
        public short fFolioDeOperacion
        {
            get { return this.FolioDeOperacion; }
            set { this.FolioDeOperacion = value; }
        }
        [Required]
        [Display(Name = "Numero De Producto")]
        public short fNumeroDeProducto
        {
            get { return this.NumeroDeProducto; }
            set { this.NumeroDeProducto = value; }
        }
        [Required]
        [Display(Name = "Codigo De Barras")]
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Tipo de Producto")]
        public short fNumeroDeTipoDeProducto
        {
            get { return this.NumeroDeTipoDeProducto; }
            set { this.NumeroDeTipoDeProducto = value; }
        }
        [Required]
        [Display(Name = "Marca")]
        public Nullable<short> fNumeroDeMarca
        {
            get { return this.NumeroDeMarca; }
            set { this.NumeroDeMarca = value; }
        }
        [Required]
        [Display(Name = "Catidad De Producto")]
        public short fCantidadDeProducto
        {
            get { return this.CantidadDeProducto; }
            set { this.CantidadDeProducto = value; }
        }
        [Required]
        [Display(Name = "Precio Unitario")]
        public decimal fPrecioUnitario
        {
            get { return this.PrecioUnitario; }
            set { this.PrecioUnitario = value; }
        }
        [Required]
        [Display(Name = "Importe De Producto")]
        public decimal fImporteDeProducto
        {
            get { return this.ImporteDeProducto; }
            set { this.ImporteDeProducto = value; }
        }
        [Required]
        [Display(Name = "Nombre De Producto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
        [Display(Name = "Tipo de Producto")]
        public String fNombreDeTipoDeProducto
        {
            get { return this.NombreDeTipoDeProducto; }
            set { this.NombreDeTipoDeProducto = value; }
        }
        [Display(Name = "Marca")]
        public String fNombreDeMarca
        {
            get { return this.NombreDeMarca; }
            set { this.NombreDeMarca = value; }
        }
    }
}