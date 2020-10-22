using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
        public class FuncionPerfilModel : Negocio.Kuup.Clases.ClsFuncionesPerfiles
    {
        public FuncionPerfilModel() { }
        public FuncionPerfilModel(Negocio.Kuup.Clases.ClsFuncionesPerfiles FuncionesPerfiles)
        {
            this.NumeroDePantalla = FuncionesPerfiles.NumeroDePantalla;
            this.NumeroDeFuncionalidad = FuncionesPerfiles.NumeroDeFuncionalidad;
            this.NumeroDePerfil = FuncionesPerfiles.NumeroDePerfil;
            this.CveEstatus = FuncionesPerfiles.CveEstatus;
            this.NombreDePantalla = FuncionesPerfiles.NombreDePantalla;
            this.NombreDeFuncionalidad = FuncionesPerfiles.NombreDeFuncionalidad;
            this.NombreDePerfil = FuncionesPerfiles.NombreDePerfil;
            this.TxtEstatus = FuncionesPerfiles.TxtEstatus;
        }
        [Required]
        [Display(Name = "Número De Pantalla")]
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
        [Display(Name = "Número De Perfil")]
        public byte fNumeroDePerfil
        {
            get { return this.NumeroDePerfil; }
            set { this.NumeroDePerfil = value; }
        }
        [Required]
        [Display(Name = "Clave De Estatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Required]
        [Display(Name = "Nombre De Pantalla")]
        public short fNombreDePantalla
        {
            get { return this.NombreDePantalla; }
            set { this.NombreDePantalla = value; }
        }
        [Required]
        [Display(Name = "Número De Funcionalidad")]
        public String fNombreDeFuncionalidad
        {
            get { return this.NombreDeFuncionalidad; }
            set { this.NombreDeFuncionalidad = value; }
        }
        [Required]
        [Display(Name = "Número De Perfil")]
        public String fNombreDePerfil
        {
            get { return this.NombreDePerfil; }
            set { this.NombreDePerfil = value; }
        }
        [Required]
        [Display(Name = "Texto De Estatus")]
        public String fTxtEstatus
        {
            get { return this.TxtEstatus; }
            set { this.TxtEstatus = value; }
        }
    }
}