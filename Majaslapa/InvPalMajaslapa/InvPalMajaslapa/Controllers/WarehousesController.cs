using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Mvc;

namespace InvPalMajaslapa.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public WarehousesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Warehouse> objWarehouseList = _db.Warehouses;
            return View(objWarehouseList);
        }
    }
}
