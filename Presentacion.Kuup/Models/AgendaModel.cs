using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class AgendaModel : Negocio.Kuup.Clases.ClsAgenda
    {
        public AgendaModel() { }
        public AgendaModel(Negocio.Kuup.Clases.ClsAgenda Agenda)
        {
            this.NumeroDeAgenda = Agenda.NumeroDeAgenda;
            this.FechaDeAlta = Agenda.FechaDeAlta;
            this.FechaDeInicioDeEvento = Agenda.FechaDeInicioDeEvento;
            this.FechaDeFinDeEvento = Agenda.FechaDeFinDeEvento;
            this.NumeroDeUsuario = Agenda.NumeroDeUsuario;
            this.CveDeNotifica = Agenda.CveDeNotifica;
            this.Descripcion = Agenda.Descripcion;
            this.CveDeEstatus = Agenda.CveDeEstatus;
            this.NombreDeUsuario = Agenda.NombreDeUsuario;
            this.TextoDeNotifica = Agenda.TextoDeNotifica;
            this.TextoDeEstatus = Agenda.TextoDeEstatus;
        }
        [Required]
        [Display(Name = "Número de Agenda")]
        public short fNumeroDeAgenda 
        { 
            get { return this.NumeroDeAgenda; }
            set { this.NumeroDeAgenda = value; } 
        }

        [Required]
        [Display(Name = "Fecha de Alta")]
        public DateTime fFechaDeAlta
        {
            get { return this.FechaDeAlta; }
            set { this.FechaDeAlta = value; }
        }

        [Required]
        [Display(Name = "Fecha de Inicio")]
        public DateTime fFechaDeInicioDeEvento
        {
            get { return this.FechaDeInicioDeEvento; }
            set { this.FechaDeInicioDeEvento = value; }
        }

        [Display(Name = "Fecha de Fin")]
        public DateTime fFechaDeFinDeEvento
        {
            get { return this.FechaDeFinDeEvento; }
            set { this.FechaDeFinDeEvento = value; }
        }

        [Required]
        [Display(Name = "Número de Usuario")]
        public short fNumeroDeUsuario
        {
            get { return this.NumeroDeUsuario; }
            set { this.NumeroDeUsuario = value; }
        }

        [Required]
        [Display(Name = "Notificar")]
        public byte fCveDeNotifica
        {
            get { return this.CveDeNotifica; }
            set { this.CveDeNotifica = value; }
        }

        [Required]
        [Display(Name = "Descripción del Evento")]
        public String fDescripcion
        {
            get { return this.Descripcion; }
            set { this.Descripcion = value; }
        }

        [Required]
        [Display(Name = "Estatus")]
        public byte fCveDeEstatus
        {
            get { return this.CveDeEstatus; }
            set { this.CveDeEstatus = value; }
        }

        [Display(Name = "Nombre de Usuario")]
        public String fNombreDeUsuario
        {
            get { return this.NombreDeUsuario; }
            set { this.NombreDeUsuario = value; }
        }

        [Display(Name = "Notificar")]
        public String fTextoDeNotifica
        {
            get { return this.TextoDeNotifica; }
            set { this.TextoDeNotifica = value; }
        }
        [Display(Name = "Estatus")]
        public String fTextoDeEstatus
        {
            get { return this.TextoDeEstatus; }
            set { this.TextoDeEstatus = value; }
        }
    }
}