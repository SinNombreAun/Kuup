using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class TipoDeProductoModel : Negocio.Kuup.Clases.ClsTiposDeProductos
    {
        public TipoDeProductoModel() { }
        public TipoDeProductoModel(Negocio.Kuup.Clases.ClsTiposDeProductos TipoDeProducto)
        {
            this.NumeroDeTipoDeProducto = TipoDeProducto.NumeroDeTipoDeProducto;
            this.NombreDeTipoDeProducto = TipoDeProducto.NombreDeTipoDeProducto;
            this.CveDeEstatus = TipoDeProducto.CveDeEstatus;
            this.TextoDeEstatus = TipoDeProducto.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Número de Tipo de Producto")]
        public short fNumeroDeTipoDeProducto
        {
            get { return this.NumeroDeTipoDeProducto; }
            set { this.NumeroDeTipoDeProducto = value; }
        }
        [Required]
        [Display(Name = "Tipo de Producto")]
        public String fNombreDeTipoDeProducto
        {
            get { return this.NombreDeTipoDeProducto; }
            set { this.NombreDeTipoDeProducto = value; }
        }

        [Required]
        [Display(Name = "Estatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Display(Name = "Estatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
}