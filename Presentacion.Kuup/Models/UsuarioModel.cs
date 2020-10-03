using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class UsuarioModel : Negocio.Kuup.Clases.ClsUsuarios
    {
        public UsuarioModel() { }
        public UsuarioModel(Negocio.Kuup.Clases.ClsUsuarios Usuario)
        {
            this.NumeroDeUsuario = Usuario.NumeroDeUsuario;
            this.NombreDeUsuario = Usuario.NombreDeUsuario;
            this.NombreDePersona = Usuario.NombreDePersona;
            this.ApellidoPaterno = Usuario.ApellidoPaterno;
            this.ApellidoMaterno = Usuario.ApellidoMaterno;
            this.CorreoDeUsuario = Usuario.CorreoDeUsuario;
            this.PasswordUsuario = Usuario.PasswordUsuario;
            this.FechaDeRegistro = Usuario.FechaDeRegistro;
            this.FechaDeCancelacion = Usuario.FechaDeCancelacion;
            this.CveDeEstatus = Usuario.CveDeEstatus;
            this.TextoDeEstatus = Usuario.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Número de Usuario")]
        public short fNumeroDeUsuario
        {
            get { return this.NumeroDeUsuario; }
            set { this.NumeroDeUsuario = value; }
        }
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public String fNombreDeUsuario
        {
            get { return this.NombreDeUsuario; }
            set { this.NombreDeUsuario = value; }
        }
        [Required]
        [Display(Name = "Nombre de Persona")]
        public String fNombreDePersona
        {
            get { return this.NombreDePersona; }
            set { this.NombreDePersona = value; }
        }
        [Required]
        [Display(Name = "Apellido Paterno")]
        public String fApellidoPaterno
        {
            get { return this.ApellidoPaterno; }
            set { this.ApellidoPaterno = value; }
        }
        [Required]
        [Display(Name = "Apellido Materno")]
        public String fApellidoMaterno
        {
            get { return this.ApellidoMaterno; }
            set { this.ApellidoMaterno = value; }
        }
        [Required]
        [Display(Name = "Correo de Usuario")]
        public String fCorreoDeUsuario
        {
            get { return this.CorreoDeUsuario; }
            set { this.CorreoDeUsuario = value; }
        }
        [Required]
        [Display(Name = "Contraseña")]
        public String fPasswordUsuario
        {
            get { return this.PasswordUsuario; }
            set { this.PasswordUsuario = value; }
        }
        [Required]
        [Display(Name = "Fecha de Registro")]
        public DateTime fFechaDeRegistro
        {
            get { return this.FechaDeRegistro; }
            set { this.FechaDeRegistro = value; }
        }
        [Display(Name = "Fecha de Cancelación")]
        public DateTime? fFechaDeCancelacion
        {
            get { return this.FechaDeCancelacion; }
            set { this.FechaDeCancelacion = value; }
        }
        [Required]
        [Display(Name="Estatus")]
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