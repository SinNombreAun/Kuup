using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class AccountModel
    {
        [Required]
        [Display(Name = "Nombre de Usuario")]
        public String NombreDeUsuario { get; set; }
        [Required]
        [Display(Name = "Password")]
        public String Password { get; set; }
        public String IP { get; set; }
        public String Terminal { get; set; }
    }
}