using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Worker
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string UserId { get; set; }
    }
}
