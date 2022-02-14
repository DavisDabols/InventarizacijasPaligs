using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Warehouse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public int MaxCapacity { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public string UserId { get; set; }
    }
}
