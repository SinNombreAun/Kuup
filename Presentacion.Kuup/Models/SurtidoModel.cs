using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class SurtidoModel : Negocio.Kuup.Clases.ClsSurtidos
    {


        public SurtidoModel() { }
        public SurtidoModel(Negocio.Kuup.Clases.ClsSurtidos Surtido)
        {
            this.FolioSurtido = Surtido.FolioSurtido;
            this.NumeroDeProveedor = Surtido.NumeroDeProveedor;
            this.NumeroDeProducto = Surtido.NumeroDeProducto;
            this.CodigoDeBarras = Surtido.CodigoDeBarras;
            this.CantNueva = Surtido.CantNueva;
            this.PrecioUnitario = Surtido.PrecioUnitario;
            this.CostoTotal = Surtido.CostoTotal;
            this.FechaDeSurtido = Surtido.FechaDeSurtido;
            this.CveEstatus = Surtido.CveEstatus;
            //this.NombreDeProveedor = Surtido.NombreDeProveedor;
            //this.NombreDeProducto = Surtido.NombreDeProducto;
            this.TxtEstatus = Surtido.TxtEstatus;
        }
        [Required]
        [Display(Name = "Folio De Surtido")]
        public short fFolioSurtido
        {
            get { return this.FolioSurtido; }
            set { this.FolioSurtido = value; }
        }
        [Required]
        [Display(Name = "Número de Proveedor")]
        public byte fNumeroDeProveedor
        {
            get { return this.NumeroDeProveedor; }
            set { this.NumeroDeProveedor = value; }
        }
        [Required]
        [Display(Name = "Número de Producto")]
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
        [Display(Name = "Cantidad Nueva")]
        public short fCantNueva
        {
            get { return this.CantNueva; }
            set { this.CantNueva = value; }
        }
        [Required]
        [Display(Name = "Precio Unitario")]
        public decimal fPrecioUnitario
        {
            get { return this.PrecioUnitario; }
            set { this.PrecioUnitario = value; }
        }
        [Required]
        [Display(Name = "Costo Total")]
        public decimal fCostoTotal
        {
            get { return this.CostoTotal; }
            set { this.CostoTotal = value; }
        }
        [Required]
        [Display(Name = "Fecha De Surtido")]
        public System.DateTime fFechaDeSurtido
        {
            get { return this.FechaDeSurtido; }
            set { this.FechaDeSurtido = value; }
        }
        [Required]
        [Display(Name = "Tipo De Surtido")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        //[Required]
        //[Display(Name = "Nombre De Proveedor")]
        //public string fNombreDeProveedor
        //{
        //    get { return this.NombreDeProveedor; }
        //    set { this.NombreDeProveedor = value; }
        //}
        //[Required]
        //[Display(Name = "Nombre De Producto")]
        //public string fNombreDeProducto
        //{
        //    get { return this.NombreDeProducto; }
        //    set { this.NombreDeProducto = value; }
        //}
        [Required]
        [Display(Name = "Tipo De Estatus")]
        public string fTxtEstatus
        {
            get { return this.TxtEstatus; }
            set { this.TxtEstatus = value; }
        }
    }
}
     