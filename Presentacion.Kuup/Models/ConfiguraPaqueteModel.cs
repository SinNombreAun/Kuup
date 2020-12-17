using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Models
{
    public class ConfiguraPaqueteModel : Negocio.Kuup.Clases.ClsConfiguraPaquetes
    {
        public ConfiguraPaqueteModel() { }
        public ConfiguraPaqueteModel(Negocio.Kuup.Clases.ClsConfiguraPaquetes ConfiguraPaquete)
        {
            this.CodigoDeBarrasPadre = ConfiguraPaquete.CodigoDeBarrasPadre;
            this.NumeroDeProductoPadre = ConfiguraPaquete.NumeroDeProductoPadre;
            this.CodigoDeBarrasHijo = ConfiguraPaquete.CodigoDeBarrasHijo;
            this.NumeroDeProductoHijo = ConfiguraPaquete.NumeroDeProductoHijo;
            this.PrecioDeProductoPadre = ConfiguraPaquete.PrecioDeProductoPadre;
            this.PrecioDeProductoHijo = ConfiguraPaquete.PrecioDeProductoHijo;
            this.CantidadASalir = ConfiguraPaquete.CantidadASalir;
            this.ImporteTotal = ConfiguraPaquete.ImporteTotal;
            this.NombreDeProductoPadre = ConfiguraPaquete.NombreDeProductoPadre;
            this.NombreDeProductoHijo = ConfiguraPaquete.NombreDeProductoHijo;
        }
        [Required]
        [Display(Name = "Codigo de Barras Padre")]
        public String fCodigoDeBarrasPadre
        {
            get { return this.CodigoDeBarrasPadre; }
            set { this.CodigoDeBarrasPadre = value; }
        }
        [Required]
        [Display(Name = "Número de Producto Padre")]
        public short fNumeroDeProductoPadre
        {
            get { return this.NumeroDeProductoPadre; }
            set { this.NumeroDeProductoPadre = value; }
        }
        [Required]
        [Display(Name = "Número de Producto Hijo")]
        public short fNumeroDeProductoHijo
        {
            get { return this.NumeroDeProductoHijo; }
            set { this.NumeroDeProductoHijo = value; }
        }
        [Required]
        [Display(Name = "Codigo de Barras Hijo")]
        public String fCodigoDeBarrasHijo
        {
            get { return this.CodigoDeBarrasHijo; }
            set { this.CodigoDeBarrasHijo = value; }
        }
        [Required]
        [Display(Name = "Precio de Producto Padre")]
        public decimal fPrecioDeProductoPadre
        {
            get { return this.PrecioDeProductoPadre; }
            set { this.PrecioDeProductoPadre = value; }
        }
        [Required]
        [Display(Name = "Precio de Producto Hijo")]
        public Nullable<decimal> fPrecioDeProductoHijo
        {
            get { return this.PrecioDeProductoHijo; }
            set { this.PrecioDeProductoHijo = value; }
        }
        [Required]
        [Display(Name = "Cantidad a Salir")]
        public byte fCantidadASalir
        {
            get { return this.CantidadASalir; }
            set { this.CantidadASalir = value; }
        }
        [Required]
        [Display(Name = "Importe Total")]
        public decimal fImporteTotal
        {
            get { return this.ImporteTotal; }
            set { this.ImporteTotal = value; }
        }
        [Display(Name = "Nombre de Producto Padre")]
        public String fNombreDeProductoPadre
        {
            get { return this.NombreDeProductoPadre; }
            set { this.NombreDeProductoPadre = value; }
        }
        [Display(Name = "Nombre de Producto Hijo")]
        public String fNombreDeProductoHijo
        {
            get { return this.NombreDeProductoHijo; }
            set { this.NombreDeProductoHijo = value; }
        }
    }
}