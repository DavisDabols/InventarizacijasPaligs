using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Vārda lauks ir obligāts")]
        public string Name { get; set; }
        public string? Surname { get; set; }
        [Required(ErrorMessage = "Epasta adrese lauks ir obligāts")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Parole lauks ir obligāts")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserId { get; set; } = "";
    }
}
