using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string CompanyName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name ="Confirm password")]
        [Compare("Password", 
            ErrorMessage ="Passwords don't match")]
        public string ConfirmPassword { get; set; }
    }
}
