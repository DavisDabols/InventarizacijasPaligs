using InvPalMajaslapa.Data;
using InvPalMajaslapa.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        //warehouselist
        //item list
        //dabū no item list cenu katram ware
        //join/viewmodel
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            IQueryable<Warehouse> objWarehouseList = _db.Warehouses.Where(w => w.UserId == user.Id);
            var warehouse = _db.Warehouses.Include(w => w.Items).Where(w => w.UserId == user.Id).ToList();
            var viewmodel = warehouse.Select(w =>
            {
                var totalPrice = w.Items.Aggregate(0m, (current, item) => current + item.Price * item.Count);
                return new WarehousesViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Address = w.Address,
                    Capacity = w.Capacity,
                    TotalPrice = totalPrice
                };
            });
            //IQueryable<Items> objItemList = _db.Items.Where(w => w.UserId == user.Id);
            //var objWarehousePrice = from i in objItemList
            //                        group i by i.WarehouseId into items
            //                        select new {
            //                            WarehouseId = items.WarehouseId,
            //                            Price = items.Sum(w => w.Price * w.Count)};
            //IQueryable<WarehousesViewModel> objWarehouse =
            //    from w in objWarehouseList
            //    join i in objWarehousePrice on w.Id equals i.WarehouseId
            //    select new WarehousesViewModel
            //    {
            //        Id = w.Id,
            //        Name = w.Name,
            //        Address = w.Address,
            //        Capacity = w.Capacity,
            //        TotalPrice = i.Price
            //    };
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

