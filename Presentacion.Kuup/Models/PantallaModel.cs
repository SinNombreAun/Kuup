using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;
namespace Presentacion.Kuup.Models
{
    public class PantallaModel : Negocio.Kuup.Clases.ClsPantallas
    {
        public PantallaModel() { }
        public PantallaModel(Negocio.Kuup.Clases.ClsPantallas Pantalla)
        {
            this.NumeroDePantalla = Pantalla.NumeroDePantalla;
            this.NombreDePantalla = Pantalla.NombreDePantalla;
            this.NombreDePantallaInt = Pantalla.NombreDePantallaInt;
            this.Descripcion = Pantalla.Descripcion;
            this.CveManejoInterno = Pantalla.CveManejoInterno;
            this.Llave = Pantalla.Llave;
            this.CveEstatus = Pantalla.CveEstatus;
            this.TextoManejoInterno = Pantalla.TextoManejoInterno;
            this.TextoEstatus = Pantalla.TextoEstatus;
        }
        [Required]
        [Display(Name = "Número de Pantalla")]
        public short fNumeroDePantalla
        {
            get { return this.NumeroDePantalla; }
            set { this.NumeroDePantalla = value; }
        }
        [Required]
        [Display(Name = "Nombre de Pantalla")]
        public String fNombreDePantalla
        {
            get { return this.NombreDePantalla; }
            set { this.NombreDePantalla = value; }
        }
        [Required]
        [Display(Name = "Número de Pantalla Int.")]
        public String fNombreDePantallaInt
        {
            get { return this.NombreDePantallaInt; }
            set { this.NombreDePantallaInt = value; }
        }
        [Required]
        [Display(Name = "Descripción de Pantalla")]
        public String fDescripcion
        {
            get { return this.Descripcion; }
            set { this.Descripcion = value; }
        }
        [Required]
        [Display(Name = "Manejo Interno")]
        public Byte fCveManejoInterno
        {
            get { return this.CveManejoInterno; }
            set { this.CveManejoInterno = value; }
        }
        [Required]
        [Display(Name = "Llave")]
        public String fLlave
        {
            get { return this.Llave; }
            set { this.Llave = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public Byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Required]
        [Display(Name = "Descripción Manejo Interno")]
        public String fTextoManejoInterno
        {
            get { return this.TextoManejoInterno; }
            set { this.TextoManejoInterno = value; }
        }
        [Required]
        [Display(Name = "Significado Del Estatus")]
        public String fTextoEstatus
        {
            get { return this.TextoEstatus; }
            set { this.TextoEstatus = value; }
        }
    }
}
