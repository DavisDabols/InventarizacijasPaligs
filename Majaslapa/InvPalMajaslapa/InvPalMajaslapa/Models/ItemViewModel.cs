namespace InvPalMajaslapa.Models
{
    public class ItemViewModel
    {
        public Items Item { get; set; }
        public Guid? WarehouseId { get; set; }

        public ItemViewModel(Items Item, Guid WarehouseId)
        {
            Item = Item;
            WarehouseId = WarehouseId;
        }
    }
}
