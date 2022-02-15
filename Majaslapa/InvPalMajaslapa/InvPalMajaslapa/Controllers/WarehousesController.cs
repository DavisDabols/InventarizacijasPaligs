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
            obj.Id = Guid.NewGuid();
            _db.Warehouses.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Edit(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var warehouseFromDb = _db.Warehouses.Find(id);
            if (warehouseFromDb == null)
            {
                return NotFound();
            }
            return View(warehouseFromDb);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Warehouse obj)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.Id = Guid.NewGuid();
            _db.Warehouses.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var warehouseFromDb = _db.Warehouses.Find(id);
            if (warehouseFromDb == null)
            {
                return NotFound();
            }
            return View(warehouseFromDb);
        }

        // POST
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id)
        {
            var obj = _db.Warehouses.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Warehouses.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

