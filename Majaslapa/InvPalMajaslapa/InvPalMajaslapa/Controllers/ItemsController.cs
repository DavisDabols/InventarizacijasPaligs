using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InvPalMajaslapa.Controllers
{
    public class ItemsController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _db;

        public ItemsController(ApplicationDbContext db,
            UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid? id)
        {
            IQueryable<Items> objItemList = _db.Items.Where(w => w.WarehouseId == id);
            ViewBag.WarehouseId = id;
            return View(objItemList);
        }

        //GET
        public IActionResult Create(Guid? id)
        {
            ViewBag.WarehouseId = id;
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Items obj, Guid warehouseId)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.Id = Guid.NewGuid();
            obj.WarehouseId = warehouseId;
            _db.Items.Add(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = warehouseId });
        }

        //GET
        public IActionResult Edit(Guid? id, Guid? warehouseId)
        {
            ViewBag.WarehouseId = warehouseId;
            if (id == null)
            {
                return NotFound();
            }
            var itemFromDb = _db.Items.Find(id);
            if (itemFromDb == null)
            {
                return NotFound();
            }
            return View(itemFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Items obj, Guid warehouseId)
        {
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.WarehouseId = warehouseId;
            _db.Items.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = warehouseId });
        }

        //GET
        public IActionResult Delete(Guid? id, Guid? warehouseId)
        {
            ViewBag.WarehouseId = warehouseId;
            if (id == null)
            {
                return NotFound();
            }
            var itemFromDb = _db.Items.Find(id);
            if (itemFromDb == null)
            {
                return NotFound();
            }
            return View(itemFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid? id, Guid warehouseId)
        {
            var obj = _db.Items.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Items.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = warehouseId });
        }
    }
}
