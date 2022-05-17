using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Items
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nosaukums lauks ir obligāts")]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public Guid WarehouseId { get; set; }
        public string UserId { get; set; } = "";
    }
}
