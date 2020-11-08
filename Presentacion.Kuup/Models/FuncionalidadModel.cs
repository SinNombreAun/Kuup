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
            this.CveDeEstatus = Funcionalidad.CveDeEstatus;
            this.TextoDeEstatus = Funcionalidad.TextoDeEstatus;
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
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
}