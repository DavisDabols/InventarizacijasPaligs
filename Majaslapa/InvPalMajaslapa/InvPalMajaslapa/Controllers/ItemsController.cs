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

        public async Task<IActionResult> Index(Guid? id, string? searchString)
        {
            IQueryable<Items> objItemList = _db.Items.Where(w => w.WarehouseId == id);
            if (searchString != null)
            {
                objItemList = objItemList.Where(i => (i.Barcode != null && i.Barcode.Contains(searchString)) || i.Name.Contains(searchString) || (i.Description != null && i.Description.Contains(searchString)));
            }
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
                var Log = new Logs { Id = Guid.NewGuid(), Name = user.Name, Surname = user.Surname, UserId = user.Id, ItemName = obj.Item.Name, Action = 'A' };
                _db.Logs.Add(Log);
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
                var Log = new Logs { Id = Guid.NewGuid(), Name = user.Name, Surname = user.Surname, UserId = user.Id, ItemName = obj.Item.Name, Action = 'E' };
                _db.Logs.Add(Log);
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
        public async Task<IActionResult> DeletePOST(Guid? id, Guid warehouseId)
        {
            var user = await userManager.GetUserAsync(User);
            var obj = _db.Items.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Items.Remove(obj);
            var Log = new Logs { Id = Guid.NewGuid(), Name = user.Name, Surname = user.Surname, UserId = user.Id, ItemName = obj.Name, Action = 'D' };
            _db.Logs.Add(Log);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = warehouseId });
        }
    }
}
