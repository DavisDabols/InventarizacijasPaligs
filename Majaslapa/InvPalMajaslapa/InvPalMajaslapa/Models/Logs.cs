namespace InvPalMajaslapa.Models
{
    public class Logs
    {
        public Guid Id { get; set; }
        public Guid WorkerId { get; set; }
        public Guid UserId { get; set; }
        public Guid ItemId { get; set; }
        // A: Pievieno
        // D: Dzēš
        // E: Rediģē
        // M: Pārvieto
        public Char Action { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
