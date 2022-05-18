using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            TempData["WarehouseId"] = id;
            return View(objItemList);
        }

        //GET
        public async Task<IActionResult> Create()
        {
            var user = await userManager.GetUserAsync(User);
            var warehouses = _db.Warehouses.Where(w => w.UserId == user.Id).ToList();
            var selectListItems = warehouses.Select(obj => new SelectListItem { Text = obj.Name, Value = obj.Id.ToString() });
            ViewBag.Warehouses = new SelectList(selectListItems, "Value", "Text");
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Items obj)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                obj.UserId = user.Id;
                obj.Id = Guid.NewGuid();
                obj.WarehouseId = (Guid)obj.WarehouseId;
                _db.Items.Add(obj);
                var warehouse = _db.Warehouses.Find(obj.WarehouseId);
                _db.Warehouses.Update(warehouse);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = obj.WarehouseId });
            }

            return View(obj);
        }

        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var itemFromDb = _db.Items.Find(id);
            if (itemFromDb == null)
            {
                return NotFound();
            }
            var user = await userManager.GetUserAsync(User);
            var warehouses = _db.Warehouses.Where(w => w.UserId == user.Id).ToList();
            var selectListItems = warehouses.Select(obj => new SelectListItem { Text = obj.Name, Value = obj.Id.ToString() });
            ViewBag.Warehouses = new SelectList(selectListItems, "Value", "Text", itemFromDb.WarehouseId);
            return View(itemFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Items obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            var user = await userManager.GetUserAsync(User);
            obj.UserId = user.Id;
            obj.WarehouseId = (Guid)obj.WarehouseId;
            _db.Items.Update(obj);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = obj.WarehouseId });
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
            var warehouse = _db.Warehouses.Find(warehouseId);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Items.Remove(obj);
            _db.Warehouses.Update(warehouse);
            _db.SaveChanges();
            return RedirectToAction("Index", new { id = warehouseId });
        }
    }
}
