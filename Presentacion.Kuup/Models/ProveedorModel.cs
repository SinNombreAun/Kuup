using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class ProveedorModel : Negocio.Kuup.Clases.ClsProveedores
    {
        public ProveedorModel() { }
        public ProveedorModel(Negocio.Kuup.Clases.ClsProveedores proveedores)
        {
            this.NumeroDeProveedor = proveedores.NumeroDeProveedor;
            this.NombreDeProveedor = proveedores.NombreDeProveedor;
            this.FechaDeRegistro = proveedores.FechaDeRegistro;
            this.CveSurtidoPorCorreo = proveedores.CveSurtidoPorCorreo;
            this.Correo = proveedores.Correo;
            this.Asunto = proveedores.Asunto;
            this.Mensaje = proveedores.Mensaje;
            this.CveEstatus = proveedores.CveEstatus;
            this.TextoSurtidoPorCorreo = proveedores.TextoSurtidoPorCorreo;
            this.TextoEstatus = proveedores.TextoEstatus;
        }
        [Required]
        [Display(Name = "Número de Proveedor")]
        public byte fNumeroDeProveedor
        {
            get { return this.NumeroDeProveedor; }
            set { this.NumeroDeProveedor = value; }
        }
        [Required]
        [Display(Name = "Nombre del Proveedor")]
        public String fNombreDeProveedor
        {
            get { return this.NombreDeProveedor; }
            set { this.NombreDeProveedor = value; }
        }
        [Required]
        [Display(Name = "Fecha de Registro")]
        public DateTime fFechaDeRegistro
        {
            get { return this.FechaDeRegistro; }
            set { this.FechaDeRegistro = value; }
        }
        [Required]
        [Display(Name = "Surtido Por Correo")]
        public Byte fCveSurtidoPorCorreo
        {
            get { return this.CveSurtidoPorCorreo; }
            set { this.CveSurtidoPorCorreo = value; }
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
        [Display(Name = "Estatus")]
        public byte fCveEstatus
        {
            get { return this.CveEstatus; }
            set { this.CveEstatus = value; }
        }
        [Required]
        [Display(Name = "Surtido Por Correo")]
        public String fTextoSurtidoPorCorreo
        {
            get { return this.TextoSurtidoPorCorreo; }
            set { this.TextoSurtidoPorCorreo = value; }
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
