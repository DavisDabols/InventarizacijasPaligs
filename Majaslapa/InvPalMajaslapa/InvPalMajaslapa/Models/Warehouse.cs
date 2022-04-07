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
        [Range(0, 2147483646, ErrorMessage = "Maksimālā ietilpība ir nepareizi ievadīta")]
        public int? MaxCapacity { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
