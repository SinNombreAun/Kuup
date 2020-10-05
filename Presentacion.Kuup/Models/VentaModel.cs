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
            this.CatProducto = Venta.CatProducto;
            this.ImporteDeProducto = Venta.ImporteDeProducto;
            this.NombreDeProducto = Venta.NombreDeProducto;
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
        public string fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Catidad De Producto")]
        public short fCatProducto
        {
            get { return this.CatProducto; }
            set { this.CatProducto = value; }
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
        public string fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
    }
}