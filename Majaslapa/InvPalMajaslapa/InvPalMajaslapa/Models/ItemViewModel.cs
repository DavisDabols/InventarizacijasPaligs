using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvPalMajaslapa.Models
{
    public class ItemViewModel
    {
        public Items? Item { get; set; }
        public Guid? WarehouseId { get; set; }
        public SelectList? selectList { get; set; }
    }
}
