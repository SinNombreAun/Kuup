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
            this.FupNumeroDePantalla = FuncionesPerfiles.FupNumeroDePantalla;
            this.FupNumeroDeFuncionalidad = FuncionesPerfiles.FupNumeroDeFuncionalidad;
            this.FupNumeroDePerfil = FuncionesPerfiles.FupNumeroDePerfil;
            this.FupCveEstatus = FuncionesPerfiles.FupCveEstatus;
            this.FupNombreDePantalla = FuncionesPerfiles.FupNombreDePantalla;
            this.FupNombreDeFuncionalidad = FuncionesPerfiles.FupNombreDeFuncionalidad;
            this.FupNombreDePerfil = FuncionesPerfiles.FupNombreDePerfil;
            this.FupTxtEstatus = FuncionesPerfiles.FupTxtEstatus;
        }
        [Required]
        [Display(Name = "Número De Pantalla")]
        public short fFupNumeroDePantalla
        {
            get { return this.FupNumeroDePantalla; }
            set { this.FupNumeroDePantalla = value; }
        }
        [Required]
        [Display(Name = "Número De Funcionalidad")]
        public byte fFupNumeroDeFuncionalidad
        {
            get { return this.FupNumeroDeFuncionalidad; }
            set { this.FupNumeroDeFuncionalidad = value; }
        }
        [Required]
        [Display(Name = "Número De Perfil")]
        public byte fFupNumeroDePerfil
        {
            get { return this.FupNumeroDePerfil; }
            set { this.FupNumeroDePerfil = value; }
        }
        [Required]
        [Display(Name = "Clave De Estatus")]
        public byte fFupCveEstatus
        {
            get { return this.FupCveEstatus; }
            set { this.FupCveEstatus = value; }
        }
        [Required]
        [Display(Name = "Nombre De Pantalla")]
        public short fFupNombreDePantalla
        {
            get { return this.FupNombreDePantalla; }
            set { this.FupNombreDePantalla = value; }
        }
        [Required]
        [Display(Name = "Número De Funcionalidad")]
        public string fFupNombreDeFuncionalidad
        {
            get { return this.FupNombreDeFuncionalidad; }
            set { this.FupNombreDeFuncionalidad = value; }
        }
        [Required]
        [Display(Name = "Número De Perfil")]
        public string fFupNombreDePerfil
        {
            get { return this.FupNombreDePerfil; }
            set { this.FupNombreDePerfil = value; }
        }
        [Required]
        [Display(Name = "Texto De Estatus")]
        public string fFupTxtEstatus
        {
            get { return this.FupTxtEstatus; }
            set { this.FupTxtEstatus = value; }
        }
    }
}