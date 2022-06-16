namespace InvPalMajaslapa.Models
{
    public class UserStatsViewModel
    {
        public ApplicationUser User { get; set; }
        public int WarehouseCount { get; set; }
        public int ItemCount { get; set; }
        public int WorkerCount { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }
}
