using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class FuncionalidadModel : Negocio.Kuup.Clases.ClsFuncionalidades
    {
        public FuncionalidadModel() { }
        public FuncionalidadModel(Negocio.Kuup.Clases.ClsFuncionalidades Funcionalidad)
        {
            this.NumeroDePantalla = Funcionalidad.NumeroDePantalla;
            this.NumeroDeFuncionalidad = Funcionalidad.NumeroDeFuncionalidad;
            this.NombreDeFuncionalidad = Funcionalidad.NombreDeFuncionalidad;
            this.CveEstatus = Funcionalidad.CveEstatus;
            this.TextoEstatus = Funcionalidad.TextoEstatus;
        }
        [Required]
        [Display(Name = "Número de Pantalla")]
        public short fNumeroDePantalla
        {
            get { return this.NumeroDePantalla; }
            set { this.NumeroDePantalla = value; }
        }
        [Required]
        [Display(Name = "Número De Funcionalidad")]
        public byte fNumeroDeFuncionalidad
        {
            get { return this.NumeroDeFuncionalidad; }
            set { this.NumeroDeFuncionalidad = value; }
        }
        [Required]
        [Display(Name = "Nombre De Funcionalidad")]
        public String fNombreDeFuncionalidad
        {
            get { return this.NombreDeFuncionalidad; }
            set { this.NombreDeFuncionalidad = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public String fTextoEstatus
        {
            get { return this.TextoEstatus; }
            set { this.TextoEstatus = value; }
        }
    }
}