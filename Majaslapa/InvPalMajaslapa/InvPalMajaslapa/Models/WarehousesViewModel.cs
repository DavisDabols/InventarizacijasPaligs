using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class WarehousesViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public int Capacity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
