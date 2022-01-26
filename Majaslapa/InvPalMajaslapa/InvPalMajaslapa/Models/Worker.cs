using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
