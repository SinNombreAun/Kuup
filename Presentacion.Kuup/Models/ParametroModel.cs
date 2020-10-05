using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices.Internal;
using System.Runtime.InteropServices;

namespace Presentacion.Kuup.Models
{
    public class ParametroModel : Negocio.Kuup.Clases.ClsParametros
    {
        public ParametroModel() { }
        public ParametroModel(Negocio.Kuup.Clases.ClsParametros Parametro)
        {
            this.CveTipo = Parametro.CveTipo;
            this.NombreDeParametro = Parametro.NombreDeParametro;
            this.ValorDeParametro = Parametro.ValorDeParametro;
            this.Descripcion = Parametro.Descripcion;
            this.TextoTipo = Parametro.TextoTipo;
        }
        [Required]
        [Display(Name = "Tipo de Parámetro")]
        public byte fCveTipo
        {
            get { return this.CveTipo; }
            set { this.CveTipo = value; }
        }
        [Required]
        [Display(Name = "Nombre de Parámetro")]
        public String fNombreDeParametro
        {
            get { return this.NombreDeParametro; }
            set { this.NombreDeParametro = value; }
        }
        [Required]
        [Display(Name = "Valor de Parámetro")]
        public String fValorDeParametro
        {
            get { return this.ValorDeParametro; }
            set { this.ValorDeParametro = value; }
        }
        [Required]
        [Display(Name = "Descripción")]
        public String fDescripcion
        {
            get { return this.Descripcion; }
            set { this.Descripcion = value; }
        }
        [Required]
        [Display(Name = "Tipo de Parámetro")]
        public String fTextoTipo
        {
            get { return this.TextoTipo; }
            set { this.TextoTipo = value; }
        }
    }
}