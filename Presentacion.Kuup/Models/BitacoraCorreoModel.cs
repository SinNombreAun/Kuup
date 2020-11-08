using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;
namespace Presentacion.Kuup.Models
{
    public class BitacoraCorreoModel : Negocio.Kuup.Clases.ClsBitacoraCorreos
    {
        public BitacoraCorreoModel() { }
        public BitacoraCorreoModel(Negocio.Kuup.Clases.ClsBitacoraCorreos BitacoraCorreo)
        {
            this.NumeroDePantalla = BitacoraCorreo.NumeroDePantalla;
            this.Correo = BitacoraCorreo.Correo;
            this.Asunto = BitacoraCorreo.Asunto;
            this.Mensaje = BitacoraCorreo.Mensaje;
            this.FechaDeEnvio = BitacoraCorreo.FechaDeEnvio;
            this.FechaDeReenvio = BitacoraCorreo.FechaDeReenvio;
            this.MensajeDeError = BitacoraCorreo.MensajeDeError;
            this.CveDeEstatus = BitacoraCorreo.CveDeEstatus;
            this.NombreDePantalla = BitacoraCorreo.NombreDePantalla;
            this.TextoDeEstatus = BitacoraCorreo.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Número de Pantalla")]
        public short fNumeroDePantalla
        {
            get { return this.NumeroDePantalla; }
            set { this.NumeroDePantalla = value; }
        }
        [Required]
        [Display(Name = "Correo")]
        public String fCorreo
        {
            get { return this.Correo; }
            set { this.Correo = value; }
        }
        [Required]
        [Display(Name = "Asunto")]
        public String fAsunto
        {
            get { return this.Asunto; }
            set { this.Asunto = value; }
        }
        [Required]
        [Display(Name = "Mensaje")]
        public String fMensaje
        {
            get { return this.Mensaje; }
            set { this.Mensaje = value; }
        }
        [Required]
        [Display(Name = "Fecha De Envío")]
        public System.DateTime fFechaDeEnvio
        {
            get { return this.FechaDeEnvio; }
            set { this.FechaDeEnvio = value; }
        }
        [Required]
        [Display(Name = "Fecha de Reenvío")]
        public System.DateTime fFechaDeReenvio
        {
            get { return this.FechaDeReenvio; }
            set { this.FechaDeReenvio = value; }
        }
        [Required]
        [Display(Name = "Mensaje De Error")]
        public String fMensajeDeError
        {
            get { return this.MensajeDeError; }
            set { this.MensajeDeError = value; }
        }
        [Required]
        [Display(Name = "Estatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }
        [Required]
        [Display(Name = "Nombre De La Pantalla")]
        public String fNombreDePantalla
        {
            get { return this.NombreDePantalla; }
            set { this.NombreDePantalla = value; }
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
