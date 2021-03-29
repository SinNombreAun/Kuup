using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class MarcaModel : Negocio.Kuup.Clases.ClsMarcas
    {
        public MarcaModel() { }
        public MarcaModel(Negocio.Kuup.Clases.ClsMarcas Marca)
        {
            this.NumeroDeMarca = Marca.NumeroDeMarca;
            this.NombreDeMarca = Marca.NombreDeMarca;
            this.CveDeEstatus = Marca.CveDeEstatus;
            this.TextoDeEstatus = Marca.TextoDeEstatus;
        }
        [Required]
        [Display(Name ="Número de Marca")]
        public short fNumeroDeMarca
        {
            get { return this.NumeroDeMarca; }
            set { this.NumeroDeMarca = value; }
        }
        [Required]
        [Display(Name = "Marca")]
        public String fNombreDeMarca
        {
            get { return this.NombreDeMarca; }
            set { this.NombreDeMarca = value; }
        }

        [Required]
        [Display(Name = "Estatus")]
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