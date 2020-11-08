using Mod.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Presentacion.Kuup.Models
{
    public class ConfiguraMayoreoModel : Negocio.Kuup.Clases.ClsConfiguraMayoreos
    {
        public ConfiguraMayoreoModel() { }
        public ConfiguraMayoreoModel(Negocio.Kuup.Clases.ClsConfiguraMayoreos ConfiguraMayoreo)
        {
            this.NumeroDeMayoreo = ConfiguraMayoreo.NumeroDeMayoreo;
            this.NumeroDeProducto = ConfiguraMayoreo.NumeroDeProducto;
            this.CodigoDeBarras = ConfiguraMayoreo.CodigoDeBarras;
            this.CveDeAplicaPaquetes = ConfiguraMayoreo.CveDeAplicaPaquetes;
            this.CantidadMinima = ConfiguraMayoreo.CantidadMinima;
            this.CantidadMaxima = ConfiguraMayoreo.CantidadMaxima;
            this.PrecioDeMayoreo = ConfiguraMayoreo.PrecioDeMayoreo;
            this.NombreDeProducto = ConfiguraMayoreo.NombreDeProducto;
        }
        [Required]
        [Display(Name = "Número de Mayoreo")]
        public short fNumeroDeMayoreo
        {
            get { return this.NumeroDeMayoreo; }
            set { this.NumeroDeMayoreo = value; }
        }
        [Required]
        [Display(Name = "Número de Producto")]
        public short fNumeroDeProducto
        {
            get { return this.NumeroDeProducto; }
            set { this.NumeroDeProducto = value; }
        }
        [Required]
        [Display(Name = "Codigo de Barras")]
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Aplica en Paquetes")]
        public byte fCveDeAplicaPaquetes
        {
            get { return this.CveDeAplicaPaquetes; }
            set { this.CveDeAplicaPaquetes = value; }
        }
        [Required]
        [Display(Name = "Cantidad Minima")]
        public byte fCantidadMinima
        {
            get { return this.CantidadMinima; }
            set { this.CantidadMinima = value; }
        }
        [Display(Name = "Cantidad Maxima")]
        public Nullable<byte> fCantidadMaxima
        {
            get { return this.CantidadMaxima; }
            set { this.CantidadMaxima = value; }
        }
        [Required]
        [Display(Name = "Precio de Mayoreo")]
        public decimal fPrecioDeMayoreo
        {
            get { return this.PrecioDeMayoreo; }
            set { this.PrecioDeMayoreo = value; }
        }
        [Display(Name = "Nombre de Producto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
    }
}