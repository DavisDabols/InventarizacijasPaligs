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
        [Required(ErrorMessage = "Skaita lauks ir obligāts")]
        [Range(1, 1000000, ErrorMessage = "Skaitam jābūt starp 1 un 1000000")]
        public int Count { get; set; }
        [Required(ErrorMessage = "Cenas lauks ir obligāts")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Cena ievadīta nepareizi")]
        public decimal Price { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public Guid WarehouseId { get; set; }
        public string UserId { get; set; } = "";
    }
}
