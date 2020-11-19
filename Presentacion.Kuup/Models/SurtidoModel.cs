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
            this.FolioDeSurtido = Surtido.FolioDeSurtido;
            this.CveDeAplicaSurtido = Surtido.CveDeAplicaSurtido;
            this.NumeroDeProveedor = Surtido.NumeroDeProveedor;
            this.NumeroDeUsuario = Surtido.NumeroDeUsuario;
            this.NumeroDeProducto = Surtido.NumeroDeProducto;
            this.CodigoDeBarras = Surtido.CodigoDeBarras;
            this.CantidadPrevia = Surtido.CantidadPrevia;
            this.CantidadNueva = Surtido.CantidadNueva;
            this.PrecioUnitario = Surtido.PrecioUnitario;
            this.CostoTotal = Surtido.CostoTotal;
            this.FechaDeSurtido = Surtido.FechaDeSurtido;
            this.CveDeEstatus = Surtido.CveDeEstatus;
            this.TextoDeAplicaProveedor = Surtido.TextoDeAplicaProveedor;
            this.NombreDeProveedor = Surtido.NombreDeProveedor;
            this.NombreDeUsuario = Surtido.NombreDeUsuario;
            this.NombreDeProducto = Surtido.NombreDeProducto;
            this.TextoDeEstatus = Surtido.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Folio De Surtido")]
        public short fFolioDeSurtido
        {
            get { return this.FolioDeSurtido; }
            set { this.FolioDeSurtido = value; }
        }
        [Required]
        [Display(Name = "Maneja Proveedor")]
        public byte fCveDeAplicaSurtido
        {
            get { return this.CveDeAplicaSurtido; }
            set { this.CveDeAplicaSurtido = value; }
        }
        [Display(Name = "Número de Proveedor")]
        public Nullable<byte> fNumeroDeProveedor
        {
            get { return this.NumeroDeProveedor; }
            set { this.NumeroDeProveedor = value; }
        }
        [Display(Name = "Número de Usuario")]
        public Nullable<short> fNumeroDeUsuario
        {
            get { return this.NumeroDeUsuario; }
            set { this.NumeroDeUsuario = value; }
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
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Cantidad Previa")]
        public short fCantidadPrevia
        {
            get { return this.CantidadPrevia; }
            set { this.CantidadPrevia = value; }
        }
        [Required]
        [Display(Name = "Cantidad Nueva")]
        public short fCantidadNueva
        {
            get { return this.CantidadNueva; }
            set { this.CantidadNueva = value; }
        }
        [Required]
        [Display(Name = "Precio Unitario")]
        public Nullable<decimal> fPrecioUnitario
        {
            get { return this.PrecioUnitario; }
            set { this.PrecioUnitario = value; }
        }
        [Required]
        [Display(Name = "Costo Total")]
        public Nullable<decimal> fCostoTotal
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
        [Display(Name = "Estatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Display(Name = "Maneja Proveedor")]
        public String fTextoDeAplicaProveedor
        {
            get { return this.TextoDeAplicaProveedor; }
            set { this.TextoDeAplicaProveedor = value; }
        }
        [Display(Name = "Nombre De Proveedor")]
        public String fNombreDeProveedor
        {
            get { return this.NombreDeProveedor; }
            set { this.NombreDeProveedor = value; }
        }
        public String fNombreDeUsuario
        {
            get { return this.NombreDeUsuario; }
            set { this.NombreDeUsuario = value; }
        }
        [Display(Name = "Nombre De Producto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
        [Display(Name = "Estatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
}
     