using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

        public async Task<IActionResult> Index(string? warehouseString, string? itemString)
        {
            var user = await userManager.GetUserAsync(User);
            var warehouse = _db.Warehouses.Include(w => w.Items).Where(w => w.UserId == user.Id).ToList();
            if (warehouseString != null)
            {
                warehouse = warehouse.Where(w => w.Name == warehouseString || w.Address == warehouseString).ToList();
            }
            if (itemString != null) 
            {
                warehouse = warehouse.Where(w => w.Items.Exists(i => i.Barcode == itemString || i.Name == itemString)).ToList();
            }
            
            var viewmodel = warehouse.Select(w =>
            {
                var totalPrice = w.Items.Aggregate(0m, (current, item) => current + item.Price * item.Count);
                var capacity = w.Items.Aggregate(0, (current, item) => current + item.Count);
                return new WarehousesViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Address = w.Address,
                    Capacity = capacity,
                    TotalPrice = totalPrice
                };
            });
            return View(viewmodel);
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
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                obj.UserId = user.Id;
                obj.Id = Guid.NewGuid();
                _db.Warehouses.Add(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(obj);
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
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(User);
                obj.UserId = user.Id;
                _db.Warehouses.Update(obj);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(obj);
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

