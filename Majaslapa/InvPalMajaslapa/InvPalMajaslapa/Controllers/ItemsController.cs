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
            var warehouseId = (Guid)TempData["WarehouseId"];
            var viewModel = new ItemViewModel { 
                WarehouseId = warehouseId, 
                selectList = new SelectList(selectListItems, "Value", "Text", warehouseId) };
            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemViewModel obj)
        {
            var user = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                obj.Item.UserId = user.Id;
                obj.Item.Id = Guid.NewGuid();
                obj.Item.WarehouseId = (Guid)obj.Item.WarehouseId;
                _db.Items.Add(obj.Item);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = obj.Item.WarehouseId });
            }
            var warehouses = _db.Warehouses.Where(w => w.UserId == user.Id).ToList();
            var selectListItems = warehouses.Select(obj => new SelectListItem { Text = obj.Name, Value = obj.Id.ToString() });
            obj.selectList = new SelectList(selectListItems, "Value", "Text", obj.Item.WarehouseId);

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
            var warehouseId = itemFromDb.WarehouseId;
            var viewModel = new ItemViewModel
            {
                Item = itemFromDb,
                WarehouseId = warehouseId,
                selectList = new SelectList(selectListItems, "Value", "Text", warehouseId)
            };
            return View(viewModel);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemViewModel obj)
        {
            var user = await userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                obj.Item.UserId = user.Id;
                _db.Items.Update(obj.Item);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = obj.Item.WarehouseId });
            }
            var warehouses = _db.Warehouses.Where(w => w.UserId == user.Id).ToList();
            var selectListItems = warehouses.Select(obj => new SelectListItem { Text = obj.Name, Value = obj.Id.ToString() });
            obj.selectList = new SelectList(selectListItems, "Value", "Text", obj.Item.WarehouseId);

            return View(obj);
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
