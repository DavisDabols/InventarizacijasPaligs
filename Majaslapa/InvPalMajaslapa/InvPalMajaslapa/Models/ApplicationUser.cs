using Microsoft.AspNetCore.Identity;

namespace InvPalMajaslapa.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? CompanyName { get; set; }
        public List<Warehouse> Warehouses { get; set; } = new();
        public List<Worker> Workers { get; set; } = new();
        public List<Items> Items { get; set; } = new();
        public List<Logs> Logs { get; set; } = new();
    }
}
