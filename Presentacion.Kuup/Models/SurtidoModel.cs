﻿using System;
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
            this.NumeroDeProveedor = Surtido.NumeroDeProveedor;
            this.NumeroDeProducto = Surtido.NumeroDeProducto;
            this.CodigoDeBarras = Surtido.CodigoDeBarras;
            this.CantNueva = Surtido.CantNueva;
            this.PrecioUnitario = Surtido.PrecioUnitario;
            this.CostoTotal = Surtido.CostoTotal;
            this.FechaDeSurtido = Surtido.FechaDeSurtido;
            this.CveDeEstatus = Surtido.CveDeEstatus;
            this.NombreDeProveedor = Surtido.NombreDeProveedor;
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
        public String fCodigoDeBarras
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
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Required]
        [Display(Name = "Nombre De Proveedor")]
        public String fNombreDeProveedor
        {
            get { return this.NombreDeProveedor; }
            set { this.NombreDeProveedor = value; }
        }
        [Required]
        [Display(Name = "Nombre De Producto")]
        public String fNombreDeProducto
        {
            get { return this.NombreDeProducto; }
            set { this.NombreDeProducto = value; }
        }
        [Required]
        [Display(Name = "Tipo De Estatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
}
     