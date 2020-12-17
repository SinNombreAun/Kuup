using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class CodigoDeBarrasModel : Negocio.Kuup.Clases.ClsCodigosDeBarras
    {
        public CodigoDeBarrasModel() { }
        public CodigoDeBarrasModel(Negocio.Kuup.Clases.ClsCodigosDeBarras CodigoDeBarras)
        {
            this.CodigoDeBarras = CodigoDeBarras.CodigoDeBarras;
            this.NumeroDeProducto = CodigoDeBarras.NumeroDeProducto;
            this.RutaDeArchivo = CodigoDeBarras.RutaDeArchivo;
            this.FechaDeGeneracion = CodigoDeBarras.FechaDeGeneracion;
            this.CveDeEstatus = CodigoDeBarras.CveDeEstatus;
            this.NombreDeProducto = CodigoDeBarras.NombreDeProducto;
            this.TextoDeEstatus = CodigoDeBarras.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Codigo de Barras")]
        public String fCodigoDeBarras
        {
            get { return this.CodigoDeBarras; }
            set { this.CodigoDeBarras = value; }
        }
        [Required]
        [Display(Name = "Número de Producto")]
        public short fNumeroDeProducto
        {
            get { return this.NumeroDeProducto; }
            set { this.NumeroDeProducto = value; }
        }
        [Required]
        [Display(Name = "Ruta de Archivo")]
        public String fRutaDeArchivo
        {
            get { return this.RutaDeArchivo; }
            set { this.RutaDeArchivo = value; }
        }
        [Required]
        [Display(Name = "Fecha de Generacion")]
        public DateTime fFechaDeGeneracion
        {
            get { return this.FechaDeGeneracion; }
            set { this.FechaDeGeneracion = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Display(Name = "Nombre de Producto")]
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