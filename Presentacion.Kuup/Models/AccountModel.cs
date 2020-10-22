using System;
using System.ComponentModel.DataAnnotations;

namespace Presentacion.Kuup.Models
{
    public class AccountModel
    {
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Account_NombreDeUsuario")]
        public String NombreDeUsuario { get; set; }
        [Required]
        [Display(ResourceType = typeof(Recursos.Textos), Name = "Account_Password")]
        public String Password { get; set; }
        public String IP { get; set; }
        public String Terminal { get; set; }
    }
}