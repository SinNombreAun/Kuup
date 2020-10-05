using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class VentaTotalModel : Negocio.Kuup.Clases.ClsVentasTotales
    {
        public VentaTotalModel() { }
        public VentaTotalModel(Negocio.Kuup.Clases.ClsVentasTotales VentaTotal)
        {
            this.FolioDeOperacion = VentaTotal.FolioDeOperacion;
            this.FechaDeOperacion = VentaTotal.FechaDeOperacion;
            this.NumeroDeUsuario = VentaTotal.NumeroDeUsuario;
            this.NombreDeCliente = VentaTotal.NombreDeCliente;
            this.ImporteBruto = VentaTotal.ImporteBruto;
            this.IVA = VentaTotal.IVA;
            this.CveAplicaDescuento = VentaTotal.CveAplicaDescuento;
            this.Porcentaje = VentaTotal.Porcentaje;
            this.ImporteNeto = VentaTotal.ImporteNeto;
            this.CveEstatus = VentaTotal.CveEstatus;
            this.AplicaDescuento = VentaTotal.AplicaDescuento;
            this.TxtEstatus = VentaTotal.TxtEstatus;
        }
        [Required]
        [Display(Name = "Folio De Operacion")]
        public short fFolioDeOperacion
        {
            get { return this.FolioDeOperacion; }
            set { this.FolioDeOperacion = value; }
        }
        [Required]
        [Display(Name = "Fecha De Operacion")]
        public System.DateTime fFechaDeOperacion
        {
            get { return this.FechaDeOperacion; }
            set { this.FechaDeOperacion = value; }
        }
        [Required]
        [Display(Name = "Número de Usuario")]
        public short fNumeroDeUsuario
        {
            get { return this.NumeroDeUsuario; }
            set { this.NumeroDeUsuario = value; }
        }
        [Required]
        [Display(Name = "Nombre De Cliente")]
        public string fNombreDeCliente
        {
            get { return this.NombreDeCliente; }
            set { this.NombreDeCliente = value; }
        }
        [Required]
        [Display(Name = "Importe Bruto")]
        public decimal fImporteBruto
        {
            get { return this.ImporteBruto; }
            set { this.ImporteBruto = value; }
        }
        [Required]
        [Display(Name = "IVA")]
        public Nullable<decimal> fIVA
        {
            get { return this.IVA; }
            set { this.IVA = value; }
        }
        [Required]
        [Display(Name = "Clave Aplica Descuento")]
        public byte fCveAplicaDescuento
        {
            get { return this.CveAplicaDescuento; }
            set { this.CveAplicaDescuento = value; }
        }
        [Required]
        [Display(Name = "Porcentaje")]
        public string fPorcentaje
        {
            get { return this.Porcentaje; }
            set { this.Porcentaje = value; }
        }
        [Required]
        [Display(Name = "Importe Neto")]
        public decimal fImporteNeto
        {
            get { return this.ImporteNeto; }
            set { this.ImporteNeto = value; }
        }
        [Required]
        [Display(Name = "Clave Estatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Required]
        [Display(Name = "AplicaDescuento")]
        public string fAplicaDescuento
        {
            get { return this.AplicaDescuento; }
            set { this.AplicaDescuento = value; }
        }
        [Required]
        [Display(Name = "Número de Producto")]
        public string fTxtEstatus
        {
            get { return this.TxtEstatus; }
            set { this.TxtEstatus = value; }
        }
    }
}