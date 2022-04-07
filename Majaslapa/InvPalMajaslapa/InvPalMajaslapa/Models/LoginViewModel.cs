using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Epasta adrese lauks ir obligāts")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parole lauks ir obligāts")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set;}
    }
}
