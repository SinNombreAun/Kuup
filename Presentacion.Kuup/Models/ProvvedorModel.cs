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
            this.NumProveedor = proveedores.NumProveedor;
            this.NombreDeProveedor = proveedores.NombreDeProveedor;
            this.FechaDeRegistro = proveedores.FechaDeRegistro;
            this.SurtidoPorCorreo = proveedores.SurtidoPorCorreo;
            this.Correo = proveedores.Correo;
            this.Asunto = proveedores.Asunto;
            this.Mensaje = proveedores.Mensaje;
            this.Estatus = proveedores.Estatus;
            this.TextoSurtidoPorCorreo = proveedores.TextoSurtidoPorCorreo;
            this.TextoEstatus = proveedores.TextoEstatus;
        }
        [Required]
        [Display(Name = "Número de Proveedor")]
        public Byte fNumProveedor
        {
            get { return this.NumProveedor; }
            set { this.NumProveedor = value; }
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
        public System.DateTime fFechaDeRegistro
        {
            get { return this.FechaDeRegistro; }
            set { this.FechaDeRegistro = value; }
        }
        [Required]
        [Display(Name = "Surtido Por Correo")]
        public Byte fSurtidoPorCorreo
        {
            get { return this.SurtidoPorCorreo; }
            set { this.SurtidoPorCorreo = value; }
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
        public Byte fEstatus
        {
            get { return this.Estatus; }
            set { this.Estatus = value; }
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
