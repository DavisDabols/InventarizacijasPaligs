using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvPalMajaslapa.Controllers
{
    public class WarehousesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _db;

        public WarehousesController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            IQueryable<Warehouse> objWarehouseList = _db.Warehouses.Where(w => w.UserId == user.Id);
            return View(objWarehouseList);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse obj)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            _db.Warehouses.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

