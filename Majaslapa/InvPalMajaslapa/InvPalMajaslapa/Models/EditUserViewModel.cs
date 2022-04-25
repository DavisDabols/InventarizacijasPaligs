using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class EditUserViewModel
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Epasta adrese lauks ir obligāts")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vecās paroles lauks ir obligāts")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Paroles lauks ir obligāts")]
        [DataType(DataType.Password)]
        [Display(Name = "Parole")]
        [MinLength(6, ErrorMessage = "Minimālais paroles garums ir 6 simboli")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(.{6,})$",
            ErrorMessage = "Parolei nepieciešams vismaz 1 lielais burts un vismaz 1 cipars")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Apstiprināt paroli lauks ir obligāts")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Paroles nav vienādas")]
        public string ConfirmPassword { get; set; }
    }
}