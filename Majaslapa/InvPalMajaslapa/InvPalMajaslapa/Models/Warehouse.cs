using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nosaukums lauks ir obligāts")]
        public string Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; } = 0;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
