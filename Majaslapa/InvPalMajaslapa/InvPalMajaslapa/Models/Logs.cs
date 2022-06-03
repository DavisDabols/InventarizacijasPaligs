using System.ComponentModel.DataAnnotations;

namespace InvPalMajaslapa.Models
{
    public class Logs
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Surname { get; set; }
        public string UserId { get; set; }
        public string ItemName { get; set; }
        // A: Pievieno
        // D: Dzēš
        // E: Rediģē
        // M: Pārvieto
        public char Action { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
